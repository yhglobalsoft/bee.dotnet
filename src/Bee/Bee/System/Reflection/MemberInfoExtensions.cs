namespace System.Reflection;

/// <summary>
/// 成员<see cref="MemberInfo"/>的扩展辅助操作方法
/// </summary>
public static class MemberInfoExtensions
{
    /// <summary>
    /// 检查指定指定类型成员中是否存在指定的Attribute特性。
    /// </summary>
    /// <typeparam name="T">要检查的Attribute特性类型。</typeparam>
    /// <param name="memberInfo">要检查的类型成员</param>
    /// <param name="inherit">是否从继承中查找</param>
    /// <returns>是否存在</returns>
    public static bool HasAttribute<T>(this MemberInfo memberInfo, bool inherit = true) where T : Attribute
    {
        return memberInfo.IsDefined(typeof(T), inherit);
    }

    /// <summary>
    /// 从类型成员获取指定Attribute特性
    /// </summary>
    /// <typeparam name="T">Attribute特性类型</typeparam>
    /// <param name="memberInfo">类型类型成员</param>
    /// <param name="inherit">是否从继承中查找</param>
    /// <returns>存在返回第一个，不存在返回null</returns>
    public static T GetAttribute<T>(this MemberInfo memberInfo, bool inherit = true) where T : Attribute
    {
        var attributes = memberInfo.GetCustomAttributes(typeof(T), inherit);
        return attributes.FirstOrDefault() as T;
    }

    /// <summary>
    /// 从类型成员获取指定Attribute特性。
    /// </summary>
    /// <typeparam name="T">Attribute特性类型。</typeparam>
    /// <param name="memberInfo">类型类型成员。</param>
    /// <param name="inherit">是否从继承中查找。</param>
    /// <returns>返回所有指定Attribute特性的数组。</returns>
    public static T[] GetAttributes<T>(this MemberInfo memberInfo, bool inherit = true) where T : Attribute
    {
        return memberInfo.GetCustomAttributes(typeof(T), inherit).Cast<T>().ToArray();
    }
}