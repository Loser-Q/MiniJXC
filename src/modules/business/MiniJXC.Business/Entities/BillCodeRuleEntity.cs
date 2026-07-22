using FreeSql.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace MiniJXC.Business.Entities;

/// <summary>
/// 编码规则配置
/// </summary>
[Table(Name = "jxc_bill_code_rule")]
public class BillCodeRuleEntity : JxcEntityBase
{
    /// <summary>
    /// 单据类型
    /// </summary>
    [Column(StringLength = 50)]
    [Required]
    public string BillType { get; set; }

    /// <summary>
    /// 规则名称
    /// </summary>
    [Column(StringLength = 100)]
    [Required]
    public string Name { get; set; }

    /// <summary>
    /// 前缀
    /// </summary>
    [Column(StringLength = 10)]
    public string? Prefix { get; set; }

    /// <summary>
    /// 日期格式
    /// </summary>
    [Column(StringLength = 20)]
    public string? DateFormat { get; set; }

    /// <summary>
    /// 流水号位数
    /// </summary>
    public int SerialLength { get; set; } = 4;

    /// <summary>
    /// 当前流水号
    /// </summary>
    public int CurrentSerial { get; set; } = 0;

    /// <summary>
    /// 示例
    /// </summary>
    [Column(StringLength = 50)]
    public string? Example { get; set; }

    /// <summary>
    /// 是否默认
    /// </summary>
    public bool IsDefault { get; set; }
}
