namespace Bee.Filter;

/// <summary>
/// 表示查询操作的前端UI操作符
/// </summary>
public class FilterOperateCodeAttribute : Attribute
{

    /// <summary>
    /// 初始化一个<see cref="FilterOperateCodeAttribute"/>类型的新实例
    /// </summary>
    public FilterOperateCodeAttribute(string code)
    {
        Code = code;
    }

    /// <summary>
    /// 获取 属性名称
    /// </summary>
    public string Code { get; }
}