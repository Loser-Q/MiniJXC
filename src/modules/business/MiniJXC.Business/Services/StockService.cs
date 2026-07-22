using MiniJXC.Business.Entities;
using ZhonTai.Admin.Core.Dto;
using ZhonTai.Admin.Services;
using ZhonTai.DynamicApi;
using ZhonTai.DynamicApi.Attributes;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using ZhonTai.Admin.Core.Repositories;
using FreeSql;
using System;
using System.Collections.Generic;

namespace MiniJXC.Business.Services;

/// <summary>
/// 盘点单服务
/// </summary>
[DynamicApi(Area = ModuleConsts.AreaName)]
public class StockCheckService : BaseService, IDynamicApi
{
    private readonly IRepositoryBase<StockCheckEntity> _repo;
    private readonly IRepositoryBase<StockCheckItemEntity> _itemRepo;
    private readonly IRepositoryBase<InventoryEntity> _inventoryRepo;
    public StockCheckService(
        IRepositoryBase<StockCheckEntity> repo,
        IRepositoryBase<StockCheckItemEntity> itemRepo,
        IRepositoryBase<InventoryEntity> inventoryRepo)
    { _repo = repo; _itemRepo = itemRepo; _inventoryRepo = inventoryRepo; }

    [HttpPost]
    public async Task<PageOutput<StockCheckEntity>> GetPageAsync(PageInput<StockCheckEntity> input)
    {
        var data = await _repo.Select.IncludeMany(a => a.Items).Include(a => a.Warehouse)
            .WhereDynamicFilter(input.DynamicFilter).Count(out var total)
            .OrderByDescending(a => a.Id).Page(input.CurrentPage, input.PageSize).ToListAsync();
        return new PageOutput<StockCheckEntity> { List = data, Total = total };
    }

    [HttpGet]
    public async Task<StockCheckEntity> GetAsync(long id)
        => await _repo.Select.IncludeMany(a => a.Items).Where(a => a.Id == id).FirstAsync();

    [HttpPost]
    public async Task<long> AddAsync(StockCheckEntity entity)
    {
        entity.BillNo = "PD-" + DateTime.Now.ToString("yyyyMMdd-HHmmss");
        entity.BillDate = entity.BillDate == default ? DateTime.Today : entity.BillDate;
        if (entity.Items?.Count > 0)
            foreach (var item in entity.Items) item.DiffQty = item.ActualQty - item.SystemQty;
        await _repo.InsertAsync(entity);
        if (entity.Items?.Count > 0)
        {
            foreach (var item in entity.Items) item.CheckId = entity.Id;
            await _itemRepo.InsertAsync(entity.Items);
        }
        return entity.Id;
    }

    [HttpPut]
    public async Task UpdateAsync(StockCheckEntity entity)
    {
        await _itemRepo.SoftDeleteAsync(a => a.CheckId == entity.Id);
        if (entity.Items?.Count > 0)
        {
            foreach (var item in entity.Items) { item.Id = 0; item.CheckId = entity.Id; item.DiffQty = item.ActualQty - item.SystemQty; }
            await _itemRepo.InsertAsync(entity.Items);
        }
        await _repo.UpdateAsync(entity);
    }

    [HttpDelete]
    public async Task DeleteAsync(long id)
    {
        var e = await _repo.GetAsync(id);
        if (e?.IsAudited == true) throw new InvalidOperationException("已审核的单据不允许删除");
        await _itemRepo.SoftDeleteAsync(a => a.CheckId == id);
        await _repo.SoftDeleteAsync(id);
    }

    /// <summary>
    /// 审核盘点单 — 盘盈入库 / 盘亏出库
    /// </summary>
    [HttpPut]
    public async Task AuditAsync(long id)
    {
        var entity = await _repo.Select.IncludeMany(a => a.Items).Where(a => a.Id == id).FirstAsync();
        if (entity == null) throw new InvalidOperationException("单据不存在");
        if (entity.IsAudited) throw new InvalidOperationException("已审核");

        foreach (var item in entity.Items)
        {
            var current = await _inventoryRepo.Select
                .Where(a => a.ProductId == item.ProductId && a.WarehouseId == entity.WarehouseId)
                .OrderByDescending(a => a.Id).FirstAsync();
            var beforeQty = current?.AfterQty ?? 0;
            var afterQty = beforeQty + item.DiffQty;

            await _inventoryRepo.InsertAsync(new InventoryEntity
            {
                ProductId = item.ProductId, WarehouseId = entity.WarehouseId,
                BillType = item.DiffQty >= 0 ? "CheckIn" : "CheckOut",
                BillId = entity.Id, BillNo = entity.BillNo,
                BeforeQty = beforeQty, ChangeQty = item.DiffQty, AfterQty = afterQty,
                BillDate = entity.BillDate, Remark = entity.Remark
            });
        }
        entity.IsAudited = true;
        await _repo.UpdateAsync(entity);
    }
}

/// <summary>
/// 调拨单服务
/// </summary>
[DynamicApi(Area = ModuleConsts.AreaName)]
public class TransferService : BaseService, IDynamicApi
{
    private readonly IRepositoryBase<TransferEntity> _repo;
    private readonly IRepositoryBase<TransferItemEntity> _itemRepo;
    private readonly IRepositoryBase<InventoryEntity> _inventoryRepo;
    public TransferService(
        IRepositoryBase<TransferEntity> repo,
        IRepositoryBase<TransferItemEntity> itemRepo,
        IRepositoryBase<InventoryEntity> inventoryRepo)
    { _repo = repo; _itemRepo = itemRepo; _inventoryRepo = inventoryRepo; }

    [HttpPost]
    public async Task<PageOutput<TransferEntity>> GetPageAsync(PageInput<TransferEntity> input)
    {
        var data = await _repo.Select.IncludeMany(a => a.Items)
            .Include(a => a.FromWarehouse).Include(a => a.ToWarehouse)
            .WhereDynamicFilter(input.DynamicFilter).Count(out var total)
            .OrderByDescending(a => a.Id).Page(input.CurrentPage, input.PageSize).ToListAsync();
        return new PageOutput<TransferEntity> { List = data, Total = total };
    }

    [HttpGet]
    public async Task<TransferEntity> GetAsync(long id)
        => await _repo.Select.IncludeMany(a => a.Items).Where(a => a.Id == id).FirstAsync();

    [HttpPost]
    public async Task<long> AddAsync(TransferEntity entity)
    {
        entity.BillNo = "DB-" + DateTime.Now.ToString("yyyyMMdd-HHmmss");
        entity.BillDate = entity.BillDate == default ? DateTime.Today : entity.BillDate;
        await _repo.InsertAsync(entity);
        if (entity.Items?.Count > 0)
        {
            foreach (var item in entity.Items) item.TransferId = entity.Id;
            await _itemRepo.InsertAsync(entity.Items);
        }
        return entity.Id;
    }

    [HttpPut]
    public async Task UpdateAsync(TransferEntity entity)
    {
        await _itemRepo.SoftDeleteAsync(a => a.TransferId == entity.Id);
        if (entity.Items?.Count > 0)
        {
            foreach (var item in entity.Items) { item.Id = 0; item.TransferId = entity.Id; }
            await _itemRepo.InsertAsync(entity.Items);
        }
        await _repo.UpdateAsync(entity);
    }

    [HttpDelete]
    public async Task DeleteAsync(long id)
    {
        var e = await _repo.GetAsync(id);
        if (e?.IsAudited == true) throw new InvalidOperationException("已审核的单据不允许删除");
        await _itemRepo.SoftDeleteAsync(a => a.TransferId == id);
        await _repo.SoftDeleteAsync(id);
    }

    /// <summary>
    /// 审核调拨单 — 调出仓库减库存 / 调入仓库加库存
    /// </summary>
    [HttpPut]
    public async Task AuditAsync(long id)
    {
        var entity = await _repo.Select.IncludeMany(a => a.Items).Where(a => a.Id == id).FirstAsync();
        if (entity == null) throw new InvalidOperationException("单据不存在");
        if (entity.IsAudited) throw new InvalidOperationException("已审核");

        foreach (var item in entity.Items)
        {
            // 调出仓库 - 减少
            var fromCurrent = await _inventoryRepo.Select
                .Where(a => a.ProductId == item.ProductId && a.WarehouseId == entity.FromWarehouseId)
                .OrderByDescending(a => a.Id).FirstAsync();
            var fromBefore = fromCurrent?.AfterQty ?? 0;
            var fromAfter = fromBefore - item.Quantity;

            await _inventoryRepo.InsertAsync(new InventoryEntity
            {
                ProductId = item.ProductId, WarehouseId = entity.FromWarehouseId,
                BillType = "TransferOut", BillId = entity.Id, BillNo = entity.BillNo,
                BeforeQty = fromBefore, ChangeQty = -item.Quantity, AfterQty = fromAfter,
                BillDate = entity.BillDate, Remark = entity.Remark
            });

            // 调入仓库 - 增加
            var toCurrent = await _inventoryRepo.Select
                .Where(a => a.ProductId == item.ProductId && a.WarehouseId == entity.ToWarehouseId)
                .OrderByDescending(a => a.Id).FirstAsync();
            var toBefore = toCurrent?.AfterQty ?? 0;
            var toAfter = toBefore + item.Quantity;

            await _inventoryRepo.InsertAsync(new InventoryEntity
            {
                ProductId = item.ProductId, WarehouseId = entity.ToWarehouseId,
                BillType = "TransferIn", BillId = entity.Id, BillNo = entity.BillNo,
                BeforeQty = toBefore, ChangeQty = item.Quantity, AfterQty = toAfter,
                BillDate = entity.BillDate, Remark = entity.Remark
            });
        }
        entity.IsAudited = true;
        await _repo.UpdateAsync(entity);
    }
}

/// <summary>
/// 编码规则服务
/// </summary>
[DynamicApi(Area = ModuleConsts.AreaName)]
public class BillCodeRuleService : BaseService, IDynamicApi
{
    private readonly IRepositoryBase<BillCodeRuleEntity> _repo;
    public BillCodeRuleService(IRepositoryBase<BillCodeRuleEntity> repo) => _repo = repo;

    [HttpPost]
    public async Task<PageOutput<BillCodeRuleEntity>> GetPageAsync(PageInput<BillCodeRuleEntity> input)
    {
        var data = await _repo.Select.WhereDynamicFilter(input.DynamicFilter)
            .Count(out var total).Page(input.CurrentPage, input.PageSize).ToListAsync();
        return new PageOutput<BillCodeRuleEntity> { List = data, Total = total };
    }

    [HttpGet]
    public async Task<BillCodeRuleEntity> GetAsync(long id) => await _repo.GetAsync(id);

    [HttpPost]
    public async Task<long> AddAsync(BillCodeRuleEntity e) { await _repo.InsertAsync(e); return e.Id; }

    [HttpPut]
    public async Task UpdateAsync(BillCodeRuleEntity e) => await _repo.UpdateAsync(e);

    [HttpDelete]
    public async Task DeleteAsync(long id) => await _repo.SoftDeleteAsync(id);

    [HttpPut]
    public async Task BatchDeleteAsync(long[] ids) => await _repo.SoftDeleteAsync(ids);
}
