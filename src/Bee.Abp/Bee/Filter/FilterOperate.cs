using System.ComponentModel;

namespace Bee.Filter;

/// <summary>
/// 筛选器操作方式
/// </summary>
public enum FilterOperate
{
    /// <summary>
    /// 并且
    /// </summary>
    [FilterOperateCode("and")]
    [Description("并且")]
    And = 1,

    /// <summary>
    /// 或者
    /// </summary>
    [FilterOperateCode("or")]
    [Description("或者")]
    Or = 2,

    /// <summary>
    /// 等于
    /// </summary>
    [FilterOperateCode("equal")]
    [Description("等于")]
    Equal = 3,

    /// <summary>
    /// 不等于
    /// </summary>
    [FilterOperateCode("not-equal")]
    [Description("不等于")]
    NotEqual = 4,

    /// <summary>
    /// 小于
    /// </summary>
    [FilterOperateCode("less")]
    [Description("小于")]
    Less = 5,

    /// <summary>
    /// 小于或等于
    /// </summary>
    [FilterOperateCode("less-or-equal")]
    [Description("小于等于")]
    LessOrEqual = 6,

    /// <summary>
    /// 大于
    /// </summary>
    [FilterOperateCode("greater")]
    [Description("大于")]
    Greater = 7,

    /// <summary>
    /// 大于或等于
    /// </summary>
    [FilterOperateCode("greater-or-equal")]
    [Description("大于等于")]
    GreaterOrEqual = 8,

    /// <summary>
    /// 以…开始
    /// </summary>
    [FilterOperateCode("starts-with")]
    [Description("开始于")]
    StartsWith = 9,

    /// <summary>
    /// 以…结束
    /// </summary>
    [FilterOperateCode("ends-with")]
    [Description("结束于")]
    EndsWith = 10,

    /// <summary>
    /// 字符串的包含（相似）
    /// </summary>
    [FilterOperateCode("contains")]
    [Description("包含")]
    Contains = 11,

    /// <summary>
    /// 字符串的不包含
    /// </summary>
    [FilterOperateCode("not-contains")]
    [Description("不包含")]
    NotContains = 12,
}