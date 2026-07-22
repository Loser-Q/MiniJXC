using FreeSql.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace MiniJXC.Business.Entities;

/// <summary>
/// 进销存实体基类（适用于非多租户场景）
/// </summary>
public class JxcEntityBase
{
    /// <summary>
    /// 主键Id
    /// </summary>
    [Column(IsPrimary = true, Position = 1)]
    public long Id { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    [Column(Position = -2, ServerTime = DateTimeKind.Local, CanUpdate = false)]
    public DateTime? CreatedTime { get; set; }

    /// <summary>
    /// 创建者用户Id
    /// </summary>
    [Column(Position = -2, CanUpdate = false)]
    public long? CreatedUserId { get; set; }

    /// <summary>
    /// 创建者用户名
    /// </summary>
    [Column(Position = -2, StringLength = 60, CanUpdate = false)]
    public string? CreatedUserName { get; set; }

    /// <summary>
    /// 修改时间
    /// </summary>
    [Column(Position = -2, ServerTime = DateTimeKind.Local)]
    public DateTime? ModifiedTime { get; set; }

    /// <summary>
    /// 修改者用户Id
    /// </summary>
    [Column(Position = -2)]
    public long? ModifiedUserId { get; set; }

    /// <summary>
    /// 修改者用户名
    /// </summary>
    [Column(Position = -2, StringLength = 60)]
    public string? ModifiedUserName { get; set; }
}
