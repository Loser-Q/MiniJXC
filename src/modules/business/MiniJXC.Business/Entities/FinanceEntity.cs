using FreeSql.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace MiniJXC.Business.Entities;

/// <summary>
/// 收款单
/// </summary>
[Table(Name = "jxc_receipt")]
public class ReceiptEntity : JxcEntityBase
{
    [Column(StringLength = 50)]
    [Required]
    public string BillNo { get; set; }

    public DateTime BillDate { get; set; } = DateTime.Today;

    /// <summary>
    /// 客户Id
    /// </summary>
    public long CustomerId { get; set; }

    [Navigate(nameof(CustomerId))]
    public ContactEntity? Customer { get; set; }

    /// <summary>
    /// 关联销货单Id
    /// </summary>
    public long? SaleId { get; set; }

    /// <summary>
    /// 收款金额
    /// </summary>
    [Column(Precision = 18, Scale = 2)]
    public decimal Amount { get; set; }

    /// <summary>
    /// 结算账户Id
    /// </summary>
    public long AccountId { get; set; }

    [Navigate(nameof(AccountId))]
    public AccountEntity? Account { get; set; }

    [Column(StringLength = 50)]
    public string? SettlementMethod { get; set; }

    [Column(StringLength = 500)]
    public string? Remark { get; set; }

    public bool IsAudited { get; set; }
}

/// <summary>
/// 付款单
/// </summary>
[Table(Name = "jxc_payment")]
public class PaymentEntity : JxcEntityBase
{
    [Column(StringLength = 50)]
    [Required]
    public string BillNo { get; set; }

    public DateTime BillDate { get; set; } = DateTime.Today;

    /// <summary>
    /// 供应商Id
    /// </summary>
    public long SupplierId { get; set; }

    [Navigate(nameof(SupplierId))]
    public ContactEntity? Supplier { get; set; }

    /// <summary>
    /// 关联购货单Id
    /// </summary>
    public long? PurchaseId { get; set; }

    [Column(Precision = 18, Scale = 2)]
    public decimal Amount { get; set; }

    public long AccountId { get; set; }

    [Navigate(nameof(AccountId))]
    public AccountEntity? Account { get; set; }

    [Column(StringLength = 50)]
    public string? SettlementMethod { get; set; }

    [Column(StringLength = 500)]
    public string? Remark { get; set; }

    public bool IsAudited { get; set; }
}
