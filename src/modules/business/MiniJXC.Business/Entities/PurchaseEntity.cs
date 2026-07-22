using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MiniJXC.Business.Entities;

/// <summary>
/// 购货单
/// </summary>
[Table(Name = "jxc_purchase")]
public class PurchaseEntity : JxcEntityBase
{
    /// <summary>
    /// 单据编号
    /// </summary>
    [Column(StringLength = 50)]
    [Required]
    public string BillNo { get; set; }

    /// <summary>
    /// 单据日期
    /// </summary>
    public DateTime BillDate { get; set; } = DateTime.Today;

    /// <summary>
    /// 供应商Id
    /// </summary>
    public long SupplierId { get; set; }

    /// <summary>
    /// 供应商
    /// </summary>
    [Navigate(nameof(SupplierId))]
    public ContactEntity? Supplier { get; set; }

    /// <summary>
    /// 仓库Id
    /// </summary>
    public long? WarehouseId { get; set; }

    /// <summary>
    /// 优惠金额
    /// </summary>
    [Column(Precision = 18, Scale = 2)]
    public decimal Discount { get; set; }

    /// <summary>
    /// 本次付款
    /// </summary>
    [Column(Precision = 18, Scale = 2)]
    public decimal PaidAmount { get; set; }

    /// <summary>
    /// 本次欠款
    /// </summary>
    [Column(Precision = 18, Scale = 2)]
    public decimal OwedAmount { get; set; }

    /// <summary>
    /// 结算账户Id
    /// </summary>
    public long? AccountId { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [Column(StringLength = 500)]
    public string? Remark { get; set; }

    /// <summary>
    /// 是否审核
    /// </summary>
    public bool IsAudited { get; set; }

    /// <summary>
    /// 明细
    /// </summary>
    [Navigate(nameof(PurchaseItemEntity.PurchaseId))]
    public List<PurchaseItemEntity> Items { get; set; }
}

/// <summary>
/// 购货单明细
/// </summary>
[Table(Name = "jxc_purchase_item")]
public class PurchaseItemEntity : JxcEntityBase
{
    /// <summary>
    /// 购货单Id
    /// </summary>
    public long PurchaseId { get; set; }

    /// <summary>
    /// 商品Id
    /// </summary>
    public long ProductId { get; set; }

    /// <summary>
    /// 商品
    /// </summary>
    [Navigate(nameof(ProductId))]
    public ProductEntity? Product { get; set; }

    /// <summary>
    /// 数量
    /// </summary>
    [Column(Precision = 18, Scale = 4)]
    public decimal Quantity { get; set; }

    /// <summary>
    /// 单价
    /// </summary>
    [Column(Precision = 18, Scale = 2)]
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// 折扣率(%)
    /// </summary>
    [Column(Precision = 18, Scale = 2)]
    public decimal DiscountRate { get; set; }

    /// <summary>
    /// 金额
    /// </summary>
    [Column(Precision = 18, Scale = 2)]
    public decimal Amount { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [Column(StringLength = 200)]
    public string? Remark { get; set; }
}
