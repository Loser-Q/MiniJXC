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
/// 收款单服务
/// </summary>
[DynamicApi(Area = ModuleConsts.AreaName)]
public class ReceiptService : BaseService, IDynamicApi
{
    private readonly IRepositoryBase<ReceiptEntity> _repo;
    private readonly IRepositoryBase<SaleEntity> _saleRepo;
    public ReceiptService(IRepositoryBase<ReceiptEntity> repo, IRepositoryBase<SaleEntity> saleRepo)
    {
        _repo = repo; _saleRepo = saleRepo;
    }

    [HttpPost]
    public async Task<PageOutput<ReceiptEntity>> GetPageAsync(PageInput<ReceiptEntity> input)
    {
        var data = await _repo.Select.Include(a => a.Customer).Include(a => a.Account)
            .WhereDynamicFilter(input.DynamicFilter).Count(out var total)
            .OrderByDescending(a => a.Id).Page(input.CurrentPage, input.PageSize).ToListAsync();
        return new PageOutput<ReceiptEntity> { List = data, Total = total };
    }

    [HttpGet]
    public async Task<ReceiptEntity> GetAsync(long id) => await _repo.GetAsync(id);

    [HttpPost]
    public async Task<long> AddAsync(ReceiptEntity entity)
    {
        entity.BillNo = await GenerateBillNoAsync("Receipt");
        entity.BillDate = entity.BillDate == default ? DateTime.Today : entity.BillDate;
        await _repo.InsertAsync(entity);
        return entity.Id;
    }

    [HttpPut]
    public async Task UpdateAsync(ReceiptEntity entity) => await _repo.UpdateAsync(entity);

    [HttpDelete]
    public async Task DeleteAsync(long id)
    {
        var e = await _repo.GetAsync(id);
        if (e?.IsAudited == true) throw new InvalidOperationException("已审核的单据不允许删除");
        await _repo.SoftDeleteAsync(id);
    }

    [HttpPut]
    public async Task BatchDeleteAsync(long[] ids) => await _repo.SoftDeleteAsync(ids);

    /// <summary>
    /// 审核收款单 — 核销销货单的应收账款
    /// </summary>
    [HttpPut]
    public async Task AuditAsync(long id)
    {
        var entity = await _repo.GetAsync(id);
        if (entity == null) throw new InvalidOperationException("单据不存在");
        if (entity.IsAudited) throw new InvalidOperationException("已审核");

        // 如果关联了销货单，核销应收
        if (entity.SaleId.HasValue)
        {
            var sale = await _saleRepo.GetAsync(entity.SaleId.Value);
            if (sale == null) throw new InvalidOperationException("关联销货单不存在");
            sale.OwedAmount = Math.Max(0, sale.OwedAmount - entity.Amount);
            await _saleRepo.UpdateAsync(sale);
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
        if (entity == null) throw new InvalidOperationException("单据不存在");
        if (!entity.IsAudited) throw new InvalidOperationException("未审核");

        if (entity.SaleId.HasValue)
        {
            var sale = await _saleRepo.GetAsync(entity.SaleId.Value);
            if (sale != null)
            {
                sale.OwedAmount += entity.Amount;
                await _saleRepo.UpdateAsync(sale);
            }
        }
        entity.IsAudited = false;
        await _repo.UpdateAsync(entity);
    }

    private async Task<string> GenerateBillNoAsync(string billType)
    {
        var rule = await this.LazyGetRequiredService<IRepositoryBase<BillCodeRuleEntity>>()
            .Select.Where(a => a.BillType == billType && a.IsDefault).FirstAsync();
        string prefix = rule?.Prefix ?? "SK";
        string dateFormat = rule?.DateFormat ?? "yyyyMMdd";
        int serialLen = rule?.SerialLength ?? 4;
        var todayStart = DateTime.Today;
        var lastBill = await _repo.Select
            .Where(a => a.BillNo.StartsWith(prefix) && a.CreatedTime >= todayStart && a.CreatedTime < todayStart.AddDays(1))
            .OrderByDescending(a => a.BillNo).FirstAsync();
        int serial = 1;
        if (lastBill != null)
        {
            var ls = lastBill.BillNo.Substring(lastBill.BillNo.Length - serialLen);
            if (int.TryParse(ls, out var n)) serial = n + 1;
        }
        return $"{prefix}-{DateTime.Now.ToString(dateFormat)}-{serial.ToString().PadLeft(serialLen, '0')}";
    }
}

/// <summary>
/// 付款单服务
/// </summary>
[DynamicApi(Area = ModuleConsts.AreaName)]
public class PaymentService : BaseService, IDynamicApi
{
    private readonly IRepositoryBase<PaymentEntity> _repo;
    private readonly IRepositoryBase<PurchaseEntity> _purchaseRepo;
    public PaymentService(IRepositoryBase<PaymentEntity> repo, IRepositoryBase<PurchaseEntity> purchaseRepo)
    {
        _repo = repo; _purchaseRepo = purchaseRepo;
    }

    [HttpPost]
    public async Task<PageOutput<PaymentEntity>> GetPageAsync(PageInput<PaymentEntity> input)
    {
        var data = await _repo.Select.Include(a => a.Supplier).Include(a => a.Account)
            .WhereDynamicFilter(input.DynamicFilter).Count(out var total)
            .OrderByDescending(a => a.Id).Page(input.CurrentPage, input.PageSize).ToListAsync();
        return new PageOutput<PaymentEntity> { List = data, Total = total };
    }

    [HttpGet]
    public async Task<PaymentEntity> GetAsync(long id) => await _repo.GetAsync(id);

    [HttpPost]
    public async Task<long> AddAsync(PaymentEntity entity)
    {
        entity.BillNo = await GenerateBillNoAsync("Payment");
        entity.BillDate = entity.BillDate == default ? DateTime.Today : entity.BillDate;
        await _repo.InsertAsync(entity);
        return entity.Id;
    }

    [HttpPut]
    public async Task UpdateAsync(PaymentEntity entity) => await _repo.UpdateAsync(entity);

    [HttpDelete]
    public async Task DeleteAsync(long id)
    {
        var e = await _repo.GetAsync(id);
        if (e?.IsAudited == true) throw new InvalidOperationException("已审核的单据不允许删除");
        await _repo.SoftDeleteAsync(id);
    }

    [HttpPut]
    public async Task BatchDeleteAsync(long[] ids) => await _repo.SoftDeleteAsync(ids);

    /// <summary>
    /// 审核付款单 — 核销购货单的应付账款
    /// </summary>
    [HttpPut]
    public async Task AuditAsync(long id)
    {
        var entity = await _repo.GetAsync(id);
        if (entity == null) throw new InvalidOperationException("单据不存在");
        if (entity.IsAudited) throw new InvalidOperationException("已审核");

        if (entity.PurchaseId.HasValue)
        {
            var purchase = await _purchaseRepo.GetAsync(entity.PurchaseId.Value);
            if (purchase == null) throw new InvalidOperationException("关联购货单不存在");
            purchase.OwedAmount = Math.Max(0, purchase.OwedAmount - entity.Amount);
            await _purchaseRepo.UpdateAsync(purchase);
        }

        entity.IsAudited = true;
        await _repo.UpdateAsync(entity);
    }

    [HttpPut]
    public async Task UnAuditAsync(long id)
    {
        var entity = await _repo.GetAsync(id);
        if (entity == null) throw new InvalidOperationException("单据不存在");
        if (!entity.IsAudited) throw new InvalidOperationException("未审核");

        if (entity.PurchaseId.HasValue)
        {
            var purchase = await _purchaseRepo.GetAsync(entity.PurchaseId.Value);
            if (purchase != null) { purchase.OwedAmount += entity.Amount; await _purchaseRepo.UpdateAsync(purchase); }
        }
        entity.IsAudited = false;
        await _repo.UpdateAsync(entity);
    }

    private async Task<string> GenerateBillNoAsync(string billType)
    {
        var rule = await this.LazyGetRequiredService<IRepositoryBase<BillCodeRuleEntity>>()
            .Select.Where(a => a.BillType == billType && a.IsDefault).FirstAsync();
        string prefix = rule?.Prefix ?? "FK";
        string dateFormat = rule?.DateFormat ?? "yyyyMMdd";
        int serialLen = rule?.SerialLength ?? 4;
        var todayStart = DateTime.Today;
        var lastBill = await _repo.Select
            .Where(a => a.BillNo.StartsWith(prefix) && a.CreatedTime >= todayStart && a.CreatedTime < todayStart.AddDays(1))
            .OrderByDescending(a => a.BillNo).FirstAsync();
        int serial = 1;
        if (lastBill != null)
        {
            var ls = lastBill.BillNo.Substring(lastBill.BillNo.Length - serialLen);
            if (int.TryParse(ls, out var n)) serial = n + 1;
        }
        return $"{prefix}-{DateTime.Now.ToString(dateFormat)}-{serial.ToString().PadLeft(serialLen, '0')}";
    }
}
