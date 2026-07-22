using MiniJXC.Business.Entities;
using ZhonTai.Admin.Core.Dto;
using ZhonTai.Admin.Services;
using ZhonTai.DynamicApi;
using ZhonTai.DynamicApi.Attributes;
using Microsoft.AspNetCore.Mvc;
using FreeSql;
using System.Threading.Tasks;
using System.Collections.Generic;
using ZhonTai.Admin.Core.Repositories;

namespace MiniJXC.Business.Services;

/// <summary>
/// 商品管理服务
/// </summary>
[DynamicApi(Area = ModuleConsts.AreaName)]
public class ProductService : BaseService, IDynamicApi
{
    private readonly IRepositoryBase<ProductEntity> _productRepo;

    public ProductService(IRepositoryBase<ProductEntity> productRepo)
    {
        _productRepo = productRepo;
    }

    /// <summary>
    /// 获取商品列表
    /// </summary>
    [HttpGet]
    public async Task<PageOutput<ProductEntity>> GetPageAsync(PageInput<ProductEntity> input)
    {
        var data = await _productRepo.Select
            .WhereDynamicFilter(input.DynamicFilter)
            .Count(out var total)
            .Page(input.CurrentPage, input.PageSize)
            .ToListAsync();

        return new PageOutput<ProductEntity> { List = data, Total = total };
    }

    /// <summary>
    /// 获取单个商品
    /// </summary>
    [HttpGet]
    public async Task<ProductEntity> GetAsync(long id)
    {
        return await _productRepo.GetAsync(id);
    }

    /// <summary>
    /// 新增商品
    /// </summary>
    [HttpPost]
    public async Task<long> AddAsync(ProductEntity entity)
    {
        await _productRepo.InsertAsync(entity);
        return entity.Id;
    }

    /// <summary>
    /// 修改商品
    /// </summary>
    [HttpPut]
    public async Task UpdateAsync(ProductEntity entity)
    {
        await _productRepo.UpdateAsync(entity);
    }

    /// <summary>
    /// 删除商品（软删除）
    /// </summary>
    [HttpDelete]
    public async Task DeleteAsync(long id)
    {
        await _productRepo.SoftDeleteAsync(id);
    }

    /// <summary>
    /// 批量删除商品（软删除）
    /// </summary>
    [HttpPut]
    public async Task BatchDeleteAsync(long[] ids)
    {
        await _productRepo.SoftDeleteAsync(ids);
    }
}
