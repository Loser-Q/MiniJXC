using FreeSql.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace MiniJXC.Business.Entities;

/// <summary>
/// 仓库
/// </summary>
[Table(Name = "jxc_warehouse")]
public class WarehouseEntity : JxcEntityBase
{
    [Column(StringLength = 50)]
    [Required]
    public string Code { get; set; }

    [Column(StringLength = 100)]
    [Required]
    public string Name { get; set; }

    [Column(StringLength = 200)]
    public string? Address { get; set; }

    [Column(StringLength = 500)]
    public string? Remark { get; set; }

    public bool IsEnabled { get; set; } = true;
}

/// <summary>
/// 结算账户
/// </summary>
[Table(Name = "jxc_account")]
public class AccountEntity : JxcEntityBase
{
    [Column(StringLength = 50)]
    [Required]
    public string Code { get; set; }

    [Column(StringLength = 100)]
    [Required]
    public string Name { get; set; }

    /// <summary>
    /// 账户类型：Cash-现金, Bank-银行, Alipay-支付宝, Wechat-微信
    /// </summary>
    [Column(StringLength = 20)]
    public string? AccountType { get; set; }

    /// <summary>
    /// 期初余额
    /// </summary>
    [Column(Precision = 18, Scale = 2)]
    public decimal InitBalance { get; set; }

    [Column(StringLength = 500)]
    public string? Remark { get; set; }

    public bool IsEnabled { get; set; } = true;
}
