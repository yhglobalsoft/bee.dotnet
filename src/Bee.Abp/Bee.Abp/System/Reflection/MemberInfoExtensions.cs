using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace System.Reflection;

/// <summary>
/// 成员<see cref="MemberInfo"/>的扩展辅助操作方法
/// </summary>
public static class MemberInfoExtensions
{
    /// <summary>
    /// 获取成员元数据的Description特性描述信息。
    /// </summary>
    /// <param name="member">成员元数据对象。</param>
    /// <param name="inherit">是否搜索成员的继承链以查找描述特性。</param>
    /// <returns>返回Description特性描述信息，如不存在则返回成员的名称。</returns>
    public static string GetDescription(this MemberInfo member, bool inherit = true)
    {
        var desc = member.GetAttribute<DescriptionAttribute>(inherit);
        if (desc != null)
        {
            return desc.Description;
        }

        var displayName = member.GetAttribute<DisplayNameAttribute>(inherit);
        if (displayName != null)
        {
            return displayName.DisplayName;
        }

        var display = member.GetAttribute<DisplayAttribute>(inherit);
        return display != null ? display.Name : member.Name;
    }
}