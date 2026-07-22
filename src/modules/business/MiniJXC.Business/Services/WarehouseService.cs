using MiniJXC.Business.Entities;
using ZhonTai.Admin.Core.Dto;
using ZhonTai.Admin.Services;
using ZhonTai.DynamicApi;
using ZhonTai.DynamicApi.Attributes;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ZhonTai.Admin.Core.Repositories;

namespace MiniJXC.Business.Services;

/// <summary>
/// 仓库管理服务
/// </summary>
[DynamicApi(Area = ModuleConsts.AreaName)]
public class WarehouseService : BaseService, IDynamicApi
{
    private readonly IRepositoryBase<WarehouseEntity> _repo;
    public WarehouseService(IRepositoryBase<WarehouseEntity> repo) => _repo = repo;

    [HttpGet]
    public async Task<PageOutput<WarehouseEntity>> GetPageAsync(PageInput<WarehouseEntity> input)
    {
        var data = await _repo.Select.WhereDynamicFilter(input.DynamicFilter)
            .Count(out var total).Page(input.CurrentPage, input.PageSize).ToListAsync();
        return new PageOutput<WarehouseEntity> { List = data, Total = total };
    }

    [HttpGet]
    public async Task<WarehouseEntity> GetAsync(long id) => await _repo.GetAsync(id);

    [HttpPost]
    public async Task<long> AddAsync(WarehouseEntity e) { await _repo.InsertAsync(e); return e.Id; }

    [HttpPut]
    public async Task UpdateAsync(WarehouseEntity e) => await _repo.UpdateAsync(e);

    /// <summary>
    /// 删除仓库（软删除）
    /// </summary>
    [HttpDelete]
    public async Task DeleteAsync(long id) => await _repo.SoftDeleteAsync(id);

    /// <summary>
    /// 批量删除仓库（软删除）
    /// </summary>
    [HttpPut]
    public async Task BatchDeleteAsync(long[] ids) => await _repo.SoftDeleteAsync(ids);
}

/// <summary>
/// 结算账户服务
/// </summary>
[DynamicApi(Area = ModuleConsts.AreaName)]
public class AccountService : BaseService, IDynamicApi
{
    private readonly IRepositoryBase<AccountEntity> _repo;
    public AccountService(IRepositoryBase<AccountEntity> repo) => _repo = repo;

    [HttpGet]
    public async Task<PageOutput<AccountEntity>> GetPageAsync(PageInput<AccountEntity> input)
    {
        var data = await _repo.Select.WhereDynamicFilter(input.DynamicFilter)
            .Count(out var total).Page(input.CurrentPage, input.PageSize).ToListAsync();
        return new PageOutput<AccountEntity> { List = data, Total = total };
    }

    [HttpGet]
    public async Task<AccountEntity> GetAsync(long id) => await _repo.GetAsync(id);

    [HttpPost]
    public async Task<long> AddAsync(AccountEntity e) { await _repo.InsertAsync(e); return e.Id; }

    [HttpPut]
    public async Task UpdateAsync(AccountEntity e) => await _repo.UpdateAsync(e);

    /// <summary>
    /// 删除账户（软删除）
    /// </summary>
    [HttpDelete]
    public async Task DeleteAsync(long id) => await _repo.SoftDeleteAsync(id);

    /// <summary>
    /// 批量删除账户（软删除）
    /// </summary>
    [HttpPut]
    public async Task BatchDeleteAsync(long[] ids) => await _repo.SoftDeleteAsync(ids);
}
