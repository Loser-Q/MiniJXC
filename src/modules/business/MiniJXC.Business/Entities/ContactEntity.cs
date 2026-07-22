using FreeSql.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace MiniJXC.Business.Entities;

/// <summary>
/// 往来单位（客户/供应商统一管理）
/// </summary>
[Table(Name = "jxc_contact")]
public class ContactEntity : JxcEntityBase
{
    /// <summary>
    /// 单位编号
    /// </summary>
    [Column(StringLength = 50)]
    [Required]
    public string Code { get; set; }

    /// <summary>
    /// 单位名称
    /// </summary>
    [Column(StringLength = 200)]
    [Required]
    public string Name { get; set; }

    /// <summary>
    /// 类型：Customer-客户, Supplier-供应商
    /// </summary>
    [Column(StringLength = 20)]
    [Required]
    public string Type { get; set; }

    /// <summary>
    /// 联系人
    /// </summary>
    [Column(StringLength = 50)]
    public string? ContactPerson { get; set; }

    /// <summary>
    /// 联系电话
    /// </summary>
    [Column(StringLength = 30)]
    public string? Phone { get; set; }

    /// <summary>
    /// 地址
    /// </summary>
    [Column(StringLength = 300)]
    public string? Address { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [Column(StringLength = 500)]
    public string? Remark { get; set; }

    /// <summary>
    /// 期初应付款（供应商）
    /// </summary>
    [Column(Precision = 18, Scale = 2)]
    public decimal? InitPayable { get; set; }

    /// <summary>
    /// 期初应收款（客户）
    /// </summary>
    [Column(Precision = 18, Scale = 2)]
    public decimal? InitReceivable { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    public bool IsEnabled { get; set; } = true;
}
