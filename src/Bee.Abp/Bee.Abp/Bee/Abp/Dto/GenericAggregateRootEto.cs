namespace Bee.Abp.Dto;

/// <summary>
/// 通用的聚合根Eto
/// </summary>
public class GenericAggregateRootEto
{
    /// <summary>
    /// 主键Id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// 类型 
    /// </summary>
    public string Type { get; set; }

    /// <summary>
    /// 内容
    /// </summary>
    public string Data { get; set; }

}