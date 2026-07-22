using MiniJXC.Business.Entities;
using ZhonTai.Admin.Core.Dto;
using ZhonTai.Admin.Services;
using ZhonTai.DynamicApi;
using ZhonTai.DynamicApi.Attributes;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using ZhonTai.Admin.Core.Repositories;
using FreeSql;
using System;

namespace MiniJXC.Business.Services;

/// <summary>
/// 购货单服务
/// </summary>
[DynamicApi(Area = ModuleConsts.AreaName)]
public class PurchaseService : BaseService, IDynamicApi
{
    private readonly IRepositoryBase<PurchaseEntity> _repo;
    private readonly IRepositoryBase<PurchaseItemEntity> _itemRepo;
    private readonly IRepositoryBase<InventoryEntity> _inventoryRepo;

    public PurchaseService(
        IRepositoryBase<PurchaseEntity> repo,
        IRepositoryBase<PurchaseItemEntity> itemRepo,
        IRepositoryBase<InventoryEntity> inventoryRepo)
    {
        _repo = repo;
        _itemRepo = itemRepo;
        _inventoryRepo = inventoryRepo;
    }

    #region CRUD

    [HttpPost]
    public async Task<PageOutput<PurchaseEntity>> GetPageAsync(PageInput<PurchaseEntity> input)
    {
        var data = await _repo.Select
            .IncludeMany(a => a.Items)
            .WhereDynamicFilter(input.DynamicFilter)
            .Count(out var total)
            .OrderByDescending(a => a.Id)
            .Page(input.CurrentPage, input.PageSize)
            .ToListAsync();
        return new PageOutput<PurchaseEntity> { List = data, Total = total };
    }

    [HttpGet]
    public async Task<PurchaseEntity> GetAsync(long id)
    {
        return await _repo.Select
            .IncludeMany(a => a.Items)
            .Where(a => a.Id == id)
            .FirstAsync();
    }

    [HttpPost]
    public async Task<long> AddAsync(PurchaseEntity entity)
    {
        // 生成单据编号
        entity.BillNo = await GenerateBillNoAsync("Purchase");
        entity.BillDate = entity.BillDate == default ? DateTime.Today : entity.BillDate;

        // 计算金额
        if (entity.Items?.Count > 0)
        {
            decimal totalAmount = 0;
            foreach (var item in entity.Items)
            {
                item.Amount = item.Quantity * item.UnitPrice * (1 - item.DiscountRate / 100);
                totalAmount += item.Amount;
            }
            entity.OwedAmount = totalAmount - entity.Discount - entity.PaidAmount;
        }

        await _repo.InsertAsync(entity);
        if (entity.Items?.Count > 0)
        {
            foreach (var item in entity.Items)
                item.PurchaseId = entity.Id;
            await _itemRepo.InsertAsync(entity.Items);
        }
        return entity.Id;
    }

    [HttpPut]
    public async Task UpdateAsync(PurchaseEntity entity)
    {
        // 删除旧明细，插入新明细
        await _itemRepo.SoftDeleteAsync(a => a.PurchaseId == entity.Id);

        decimal totalAmount = 0;
        if (entity.Items?.Count > 0)
        {
            foreach (var item in entity.Items)
            {
                item.Id = 0;
                item.PurchaseId = entity.Id;
                item.Amount = item.Quantity * item.UnitPrice * (1 - item.DiscountRate / 100);
                totalAmount += item.Amount;
            }
            await _itemRepo.InsertAsync(entity.Items);
        }
        entity.OwedAmount = totalAmount - entity.Discount - entity.PaidAmount;
        await _repo.UpdateAsync(entity);
    }

    [HttpDelete]
    public async Task DeleteAsync(long id)
    {
        var entity = await _repo.GetAsync(id);
        if (entity?.IsAudited == true)
            throw new InvalidOperationException("已审核的单据不允许删除");
        await _itemRepo.SoftDeleteAsync(a => a.PurchaseId == id);
        await _repo.SoftDeleteAsync(id);
    }

    [HttpPut]
    public async Task BatchDeleteAsync(long[] ids)
    {
        foreach (var id in ids)
        {
            var entity = await _repo.GetAsync(id);
            if (entity?.IsAudited == true)
                throw new InvalidOperationException($"单据 [{entity.BillNo}] 已审核，不允许删除");
        }
        foreach (var id in ids)
            await _itemRepo.SoftDeleteAsync(a => a.PurchaseId == id);
        await _repo.SoftDeleteAsync(ids);
    }

    #endregion

    #region 业务：审核

    /// <summary>
    /// 审核购货单 — 库存增加 + 产生应付
    /// </summary>
    [HttpPut]
    public async Task AuditAsync(long id)
    {
        var entity = await _repo.Select
            .IncludeMany(a => a.Items)
            .Where(a => a.Id == id)
            .FirstAsync();

        if (entity == null)
            throw new InvalidOperationException("单据不存在");
        if (entity.IsAudited)
            throw new InvalidOperationException("单据已审核，不能重复审核");

        var warehouseId = entity.WarehouseId ?? 0;
        if (warehouseId == 0)
            throw new InvalidOperationException("请选择仓库");

        // 写入库存流水
        var now = DateTime.Now;
        foreach (var item in entity.Items)
        {
            // 获取当前库存
            var currentInventory = await _inventoryRepo.Select
                .Where(a => a.ProductId == item.ProductId && a.WarehouseId == warehouseId)
                .OrderByDescending(a => a.Id)
                .FirstAsync();

            var beforeQty = currentInventory?.AfterQty ?? 0;
            var afterQty = beforeQty + item.Quantity;

            await _inventoryRepo.InsertAsync(new InventoryEntity
            {
                ProductId = item.ProductId,
                WarehouseId = warehouseId,
                BillType = "Purchase",
                BillId = entity.Id,
                BillNo = entity.BillNo,
                BeforeQty = beforeQty,
                ChangeQty = item.Quantity,
                AfterQty = afterQty,
                UnitCost = item.UnitPrice,
                BillDate = entity.BillDate,
                Remark = entity.Remark
            });
        }

        entity.IsAudited = true;
        await _repo.UpdateAsync(entity);
    }

    /// <summary>
    /// 反审核
    /// </summary>
    [HttpPut]
    public async Task UnAuditAsync(long id)
    {
        var entity = await _repo.GetAsync(id);
        if (entity == null)
            throw new InvalidOperationException("单据不存在");
        if (!entity.IsAudited)
            throw new InvalidOperationException("单据未审核");

        // 删除库存流水
        await _inventoryRepo.SoftDeleteAsync(a => a.BillType == "Purchase" && a.BillId == id);

        entity.IsAudited = false;
        await _repo.UpdateAsync(entity);
    }

    #endregion

    #region 辅助

    private async Task<string> GenerateBillNoAsync(string billType)
    {
        var rule = await this.LazyGetRequiredService<IRepositoryBase<BillCodeRuleEntity>>()
            .Select.Where(a => a.BillType == billType && a.IsDefault).FirstAsync();

        string prefix = rule?.Prefix ?? "CG";
        string dateFormat = rule?.DateFormat ?? "yyyyMMdd";
        int serialLen = rule?.SerialLength ?? 4;

        var todayStart = DateTime.Today;
        var todayEnd = todayStart.AddDays(1);

        // 获取今天最大的编号
        var lastBill = await _repo.Select
            .Where(a => a.BillNo.StartsWith(prefix) && a.CreatedTime >= todayStart && a.CreatedTime < todayEnd)
            .OrderByDescending(a => a.BillNo)
            .FirstAsync();

        int serial = 1;
        if (lastBill != null)
        {
            var lastSerial = lastBill.BillNo.Substring(lastBill.BillNo.Length - serialLen);
            if (int.TryParse(lastSerial, out var lastNum))
                serial = lastNum + 1;
        }

        // 更新规则中的当前序号
        if (rule != null)
        {
            rule.CurrentSerial = serial;
            await this.LazyGetRequiredService<IRepositoryBase<BillCodeRuleEntity>>().UpdateAsync(rule);
        }

        return $"{prefix}-{DateTime.Now.ToString(dateFormat)}-{serial.ToString().PadLeft(serialLen, '0')}";
    }

    #endregion
}
