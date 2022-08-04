using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace System;

public static class TypeInfoExtensions
{
    /// <summary>
    /// 判断类型是否为聚合根
    /// </summary>
    public static bool IsAggregateRoot(this TypeInfo typeInfo)
    {
        return typeInfo.IsAssignableFrom(typeof(FullAuditedAggregateRoot)) && !typeInfo.IsAbstract;
    }
    
    /// <summary>
    /// 判断类型是否为实体 
    /// </summary>
    public static bool IsEntity(this TypeInfo typeInfo)
    {
        return typeInfo.IsAssignableTo(typeof(IEntity)) && 
               !typeInfo.IsAbstract && 
               typeInfo.IsClass;
    }
}