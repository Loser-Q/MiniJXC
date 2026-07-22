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
/// 库存查询服务（只读）
/// </summary>
[DynamicApi(Area = ModuleConsts.AreaName)]
public class InventoryService : BaseService, IDynamicApi
{
    private readonly IRepositoryBase<InventoryEntity> _repo;
    public InventoryService(IRepositoryBase<InventoryEntity> repo) => _repo = repo;

    /// <summary>
    /// 库存流水列表
    /// </summary>
    [HttpPost]
    public async Task<PageOutput<InventoryEntity>> GetPageAsync(PageInput<InventoryEntity> input)
    {
        var data = await _repo.Select
            .Include(a => a.Product)
            .Include(a => a.Warehouse)
            .WhereDynamicFilter(input.DynamicFilter)
            .Count(out var total)
            .OrderByDescending(a => a.Id)
            .Page(input.CurrentPage, input.PageSize)
            .ToListAsync();
        return new PageOutput<InventoryEntity> { List = data, Total = total };
    }

    /// <summary>
    /// 当前库存查询（按商品+仓库汇总）
    /// </summary>
    [HttpPost]
    public async Task<List<InventoryBalanceOutput>> GetBalanceAsync(PageInput<InventoryEntity> input)
    {
        var query = _repo.Select
            .Include(a => a.Product)
            .Include(a => a.Warehouse);

        if (input.Filter != null)
        {
            query = query.WhereDynamicFilter(input.DynamicFilter);
        }

        // 按商品+仓库分组，取最新 AfterQty
        var all = await query.OrderByDescending(a => a.Id).ToListAsync();
        var balance = all.GroupBy(a => new { a.ProductId, a.WarehouseId })
            .Select(g => g.First())
            .Where(a => a.AfterQty != 0)
            .Select(a => new InventoryBalanceOutput
            {
                ProductId = a.ProductId,
                ProductName = a.Product?.Name,
                ProductCode = a.Product?.Code,
                WarehouseId = a.WarehouseId,
                WarehouseName = a.Warehouse?.Name,
                Quantity = a.AfterQty,
                Unit = a.Product?.Unit
            })
            .OrderBy(a => a.ProductName)
            .ToList();
        return balance;
    }
}

/// <summary>
/// 库存结存输出
/// </summary>
public class InventoryBalanceOutput
{
    public long ProductId { get; set; }
    public string? ProductName { get; set; }
    public string? ProductCode { get; set; }
    public long WarehouseId { get; set; }
    public string? WarehouseName { get; set; }
    public decimal Quantity { get; set; }
    public string? Unit { get; set; }
}
