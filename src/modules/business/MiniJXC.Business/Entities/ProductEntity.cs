using FreeSql.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace MiniJXC.Business.Entities;

/// <summary>
/// 商品
/// </summary>
[Table(Name = "jxc_product")]
public class ProductEntity : JxcEntityBase
{
    /// <summary>
    /// 商品编号
    /// </summary>
    [Column(StringLength = 50)]
    [Required]
    public string Code { get; set; }

    /// <summary>
    /// 商品名称
    /// </summary>
    [Column(StringLength = 200)]
    [Required]
    public string Name { get; set; }

    /// <summary>
    /// 商品类别
    /// </summary>
    [Column(StringLength = 100)]
    public string? Category { get; set; }

    /// <summary>
    /// 规格型号
    /// </summary>
    [Column(StringLength = 100)]
    public string? Specification { get; set; }

    /// <summary>
    /// 计量单位
    /// </summary>
    [Column(StringLength = 20)]
    public string? Unit { get; set; }

    /// <summary>
    /// 条码
    /// </summary>
    [Column(StringLength = 100)]
    public string? Barcode { get; set; }

    /// <summary>
    /// 采购价
    /// </summary>
    [Column(Precision = 18, Scale = 2)]
    public decimal? PurchasePrice { get; set; }

    /// <summary>
    /// 销售价
    /// </summary>
    [Column(Precision = 18, Scale = 2)]
    public decimal? SalePrice { get; set; }

    /// <summary>
    /// 初始库存数量
    /// </summary>
    [Column(Precision = 18, Scale = 4)]
    public decimal InitStock { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [Column(StringLength = 500)]
    public string? Remark { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    public bool IsEnabled { get; set; } = true;
}
