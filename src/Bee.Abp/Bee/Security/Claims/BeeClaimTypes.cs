namespace Bee.Security.Claims;

/// <summary>
/// Bee 框架支持的Claim类型常量
/// </summary>
public static class BeeClaimTypes
{
    /// <summary>
    /// Claim中仓库Id Key
    /// </summary>
    public const string DefaultClaimWarehouseId = "warehouse_id";
        
    /// <summary>
    /// Claim中是否越海内部用户 Key
    /// 用来区分内部或外部用户
    /// </summary>
    public const string DefaultIsInternalUser = "is_internal_user";
        
    /// <summary>
    /// 仓库Id
    /// 默认值: "warehouse_id".
    /// </summary>
    public static string WarehouseId { get; set; } =  DefaultClaimWarehouseId;
        
    /// <summary>
    /// 是否内部用户
    /// 默认值: "is_internal_user".
    /// </summary>
    public static string IsInternalUser { get; set; } =  DefaultIsInternalUser;
        
}