using FreeSql.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace MiniJXC.Business.Entities;

/// <summary>
/// 库存记录（出入库流水）
/// </summary>
[Table(Name = "jxc_inventory")]
public class InventoryEntity : JxcEntityBase
{
    public long ProductId { get; set; }

    [Navigate(nameof(ProductId))]
    public ProductEntity? Product { get; set; }

    public long WarehouseId { get; set; }

    [Navigate(nameof(WarehouseId))]
    public WarehouseEntity? Warehouse { get; set; }

    /// <summary>
    /// 单据类型：Purchase-购货, Sale-销货, CheckIn-盘盈入库, CheckOut-盘亏出库, TransferIn-调拨入库, TransferOut-调拨出库, OtherIn-其他入库, OtherOut-其他出库
    /// </summary>
    [Column(StringLength = 20)]
    [Required]
    public string BillType { get; set; }

    /// <summary>
    /// 关联单据Id
    /// </summary>
    public long? BillId { get; set; }

    /// <summary>
    /// 单据编号
    /// </summary>
    [Column(StringLength = 50)]
    public string? BillNo { get; set; }

    /// <summary>
    /// 变动前数量
    /// </summary>
    [Column(Precision = 18, Scale = 4)]
    public decimal BeforeQty { get; set; }

    /// <summary>
    /// 变动数量（正=入库，负=出库）
    /// </summary>
    [Column(Precision = 18, Scale = 4)]
    public decimal ChangeQty { get; set; }

    /// <summary>
    /// 变动后数量
    /// </summary>
    [Column(Precision = 18, Scale = 4)]
    public decimal AfterQty { get; set; }

    /// <summary>
    /// 单价/成本
    /// </summary>
    [Column(Precision = 18, Scale = 2)]
    public decimal? UnitCost { get; set; }

    public DateTime BillDate { get; set; }

    [Column(StringLength = 200)]
    public string? Remark { get; set; }
}

/// <summary>
/// 库存盘点单
/// </summary>
[Table(Name = "jxc_stock_check")]
public class StockCheckEntity : JxcEntityBase
{
    [Column(StringLength = 50)]
    [Required]
    public string BillNo { get; set; }

    public DateTime BillDate { get; set; } = DateTime.Today;

    public long WarehouseId { get; set; }

    [Navigate(nameof(WarehouseId))]
    public WarehouseEntity? Warehouse { get; set; }

    [Column(StringLength = 500)]
    public string? Remark { get; set; }

    public bool IsAudited { get; set; }

    [Navigate(nameof(StockCheckItemEntity.CheckId))]
    public List<StockCheckItemEntity> Items { get; set; }
}

/// <summary>
/// 盘点单明细
/// </summary>
[Table(Name = "jxc_stock_check_item")]
public class StockCheckItemEntity : JxcEntityBase
{
    public long CheckId { get; set; }
    public long ProductId { get; set; }

    [Navigate(nameof(ProductId))]
    public ProductEntity? Product { get; set; }

    /// <summary>
    /// 系统库存数量
    /// </summary>
    [Column(Precision = 18, Scale = 4)]
    public decimal SystemQty { get; set; }

    /// <summary>
    /// 实际盘点数量
    /// </summary>
    [Column(Precision = 18, Scale = 4)]
    public decimal ActualQty { get; set; }

    /// <summary>
    /// 盈亏数量
    /// </summary>
    [Column(Precision = 18, Scale = 4)]
    public decimal DiffQty { get; set; }

    [Column(StringLength = 200)]
    public string? Remark { get; set; }
}

/// <summary>
/// 仓库调拨单
/// </summary>
[Table(Name = "jxc_transfer")]
public class TransferEntity : JxcEntityBase
{
    [Column(StringLength = 50)]
    [Required]
    public string BillNo { get; set; }

    public DateTime BillDate { get; set; } = DateTime.Today;

    /// <summary>
    /// 调出仓库Id
    /// </summary>
    public long FromWarehouseId { get; set; }

    [Navigate(nameof(FromWarehouseId))]
    public WarehouseEntity? FromWarehouse { get; set; }

    /// <summary>
    /// 调入仓库Id
    /// </summary>
    public long ToWarehouseId { get; set; }

    [Navigate(nameof(ToWarehouseId))]
    public WarehouseEntity? ToWarehouse { get; set; }

    [Column(StringLength = 500)]
    public string? Remark { get; set; }

    public bool IsAudited { get; set; }

    [Navigate(nameof(TransferItemEntity.TransferId))]
    public List<TransferItemEntity> Items { get; set; }
}

/// <summary>
/// 调拨单明细
/// </summary>
[Table(Name = "jxc_transfer_item")]
public class TransferItemEntity : JxcEntityBase
{
    public long TransferId { get; set; }
    public long ProductId { get; set; }

    [Navigate(nameof(ProductId))]
    public ProductEntity? Product { get; set; }

    [Column(Precision = 18, Scale = 4)]
    public decimal Quantity { get; set; }

    [Column(StringLength = 200)]
    public string? Remark { get; set; }
}
