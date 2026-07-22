using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MiniJXC.Business.Entities;

/// <summary>
/// 销货单
/// </summary>
[Table(Name = "jxc_sale")]
public class SaleEntity : JxcEntityBase
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

    public long? WarehouseId { get; set; }

    [Column(Precision = 18, Scale = 2)]
    public decimal Discount { get; set; }

    [Column(Precision = 18, Scale = 2)]
    public decimal ReceivedAmount { get; set; }

    [Column(Precision = 18, Scale = 2)]
    public decimal OwedAmount { get; set; }

    public long? AccountId { get; set; }

    [Column(StringLength = 500)]
    public string? Remark { get; set; }

    public bool IsAudited { get; set; }

    [Navigate(nameof(SaleItemEntity.SaleId))]
    public List<SaleItemEntity> Items { get; set; }
}

/// <summary>
/// 销货单明细
/// </summary>
[Table(Name = "jxc_sale_item")]
public class SaleItemEntity : JxcEntityBase
{
    public long SaleId { get; set; }
    public long ProductId { get; set; }

    [Navigate(nameof(ProductId))]
    public ProductEntity? Product { get; set; }

    [Column(Precision = 18, Scale = 4)]
    public decimal Quantity { get; set; }

    [Column(Precision = 18, Scale = 2)]
    public decimal UnitPrice { get; set; }

    [Column(Precision = 18, Scale = 2)]
    public decimal DiscountRate { get; set; }

    [Column(Precision = 18, Scale = 2)]
    public decimal Amount { get; set; }

    [Column(StringLength = 200)]
    public string? Remark { get; set; }
}
