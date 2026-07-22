using MiniJXC.Business.Entities;
using ZhonTai.Admin.Core.Dto;
using ZhonTai.Admin.Services;
using ZhonTai.DynamicApi;
using ZhonTai.DynamicApi.Attributes;
using Microsoft.AspNetCore.Mvc;
using FreeSql;
using System.Threading.Tasks;
using ZhonTai.Admin.Core.Repositories;

namespace MiniJXC.Business.Services;

/// <summary>
/// 往来单位服务（客户+供应商）
/// </summary>
[DynamicApi(Area = ModuleConsts.AreaName)]
public class ContactService : BaseService, IDynamicApi
{
    private readonly IRepositoryBase<ContactEntity> _contactRepo;

    public ContactService(IRepositoryBase<ContactEntity> contactRepo)
    {
        _contactRepo = contactRepo;
    }

    [HttpGet]
    public async Task<PageOutput<ContactEntity>> GetPageAsync(PageInput<ContactEntity> input)
    {
        var data = await _contactRepo.Select
            .WhereDynamicFilter(input.DynamicFilter)
            .Count(out var total)
            .Page(input.CurrentPage, input.PageSize)
            .ToListAsync();

        return new PageOutput<ContactEntity> { List = data, Total = total };
    }

    [HttpGet]
    public async Task<ContactEntity> GetAsync(long id)
    {
        return await _contactRepo.GetAsync(id);
    }

    [HttpPost]
    public async Task<long> AddAsync(ContactEntity entity)
    {
        await _contactRepo.InsertAsync(entity);
        return entity.Id;
    }

    [HttpPut]
    public async Task UpdateAsync(ContactEntity entity)
    {
        await _contactRepo.UpdateAsync(entity);
    }

    /// <summary>
    /// 删除往来单位（软删除）
    /// </summary>
    [HttpDelete]
    public async Task DeleteAsync(long id)
    {
        await _contactRepo.SoftDeleteAsync(id);
    }

    /// <summary>
    /// 批量删除往来单位（软删除）
    /// </summary>
    [HttpPut]
    public async Task BatchDeleteAsync(long[] ids)
    {
        await _contactRepo.SoftDeleteAsync(ids);
    }
}
