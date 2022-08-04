namespace Bee.Abp.Data;

public static class BeeCommonDbProperties
{
    /// <summary>
    /// 数据库表名前缀 仅使用在Bee的应用模块(AuditQuery,DataDictionary,ImportExport...)
    /// 默认值: "Bee"
    /// </summary>
    public static string DbTablePrefix { get; set; } = "Bee";

    /// <summary>
    /// 数据库 Schema 仅使用在Bee的应用模块(AuditQuery,DataDictionary,ImportExport...)
    /// 默认值: null
    /// </summary>
    public static string DbSchema { get; set; } = null;
}