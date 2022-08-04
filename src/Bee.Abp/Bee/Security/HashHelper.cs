using System.Security.Cryptography;
using System.Text;

namespace Bee.Security;

/// <summary>
/// 字符串Hash操作类
/// </summary>
public static class HashHelper
{
    /// <summary>
    /// 获取字符串的Sha256哈希值，默认编码为<see cref="Encoding.UTF8"/>
    /// </summary>
    public static string GetSha256(string value, Encoding encoding = null)
    {
        Guard.NotNull(value, nameof(value));

        var hash = SHA256.Create();
        var sb = new StringBuilder();
        encoding ??= Encoding.UTF8;
        var bytes = hash.ComputeHash(encoding.GetBytes(value));
        foreach (var b in bytes)
        {
            sb.AppendFormat("{0:x2}", b);
        }

        return sb.ToString();
    }

    /// <summary>
    /// 获取字符串的Sha512哈希值，默认编码为<see cref="Encoding.UTF8"/>
    /// </summary>
    public static string GetSha512(string value, Encoding encoding = null)
    {
        Guard.NotNull(value, nameof(value));

        var sb = new StringBuilder();
        var hash = SHA512.Create();
        encoding ??= Encoding.UTF8;
        var bytes = hash.ComputeHash(encoding.GetBytes(value));
        foreach (var b in bytes)
        {
            sb.AppendFormat("{0:x2}", b);
        }

        return sb.ToString();
    }
}