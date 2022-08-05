using System.Diagnostics;


namespace System.Reflection;

/// <summary>
/// 程序集扩展操作类
/// </summary>
public static class AssemblyExtensions
{
    /// <summary>
    /// 获取程序集的产品版本
    /// </summary>
    public static string GetProductVersion(this Assembly assembly)
    {
        if (assembly is null) throw new ArgumentNullException();
        var info = FileVersionInfo.GetVersionInfo(assembly.Location);
        var version = info.ProductVersion;
        if (version.Contains("+"))
        {
            version = version.ReplaceRegex(@"\+(\w+)?", "");
        }

        return version;
    }
}