using System.Diagnostics;
using System.Linq;
using Bee.Abp.Exceptions;
using Bee.Abp.Exceptions.Domains;

namespace Bee.Abp;

/// <summary>
/// 领域层对象检查
/// </summary>
[DebuggerStepThrough]
public static class DomainGuard
{
    /// <summary>
    /// 检查字符串不能为空引用或空字符串，否则抛出
    /// <see cref="BeeNullOrEmptyDomainException"/>异常
    /// 或<see cref="BeeLengthGreaterDomainException"/>异常
    /// 或<see cref="BeeLengthLessDomainException"/>异常
    /// </summary>
    /// <param name="value"></param>
    /// <param name="valueName">参数名称</param>
    /// <param name="maxLength">字符串允许的最大长度</param>
    /// <param name="minLength">字符串允许的最小长度 0表示不限制最小长度</param>
    /// <exception cref="BeeNullOrEmptyDomainException"></exception>
    /// <exception cref="BeeLengthGreaterDomainException"></exception>
    /// <exception cref="BeeLengthLessDomainException"></exception>
    public static string NotNullOrEmpty(
        string value,
        string valueName,
        int maxLength = int.MaxValue,
        int minLength = 0)
    {
        if (value.IsNullOrEmpty())
        {
            throw new BeeNullOrEmptyDomainException(valueName);
        }

        if (value.Length > maxLength)
        {
            throw new BeeLengthGreaterDomainException(valueName, maxLength);
        }

        if (minLength > 0 && value.Length < minLength)
        {
            throw new BeeLengthLessDomainException(valueName, minLength);
        }

        return value;
    }

    /// <summary>
    /// 检查字符串不能为空引用或全部为空白，否则抛出
    /// <see cref="BeeNullOrWhiteSpaceDomainException"/>异常
    /// 或<see cref="BeeLengthGreaterDomainException"/>异常
    /// 或<see cref="BeeLengthLessDomainException"/>异常
    /// </summary>
    /// <param name="value">需检查的字符串</param>
    /// <param name="valueName">参数名称</param>
    /// <param name="maxLength">字符串允许的最大长度</param>
    /// <param name="minLength">字符串允许的最小长度 0表示不限制最小长度</param>
    /// <exception cref="BeeNullOrWhiteSpaceDomainException"></exception>
    /// <exception cref="BeeLengthGreaterDomainException"></exception>
    /// <exception cref="BeeLengthLessDomainException"></exception>
    public static string NotNullOrWhiteSpace(
        string value,
        string valueName,
        int maxLength = int.MaxValue,
        int minLength = 0)
    {
        if (value.IsNullOrWhiteSpace())
        {
            throw new BeeNullOrWhiteSpaceDomainException(valueName);
        }

        if (value.Length > maxLength)
        {
            throw new BeeLengthGreaterDomainException(valueName, maxLength);
        }

        if (minLength > 0 && value.Length < minLength)
        {
            throw new BeeLengthLessDomainException(valueName, minLength);
        }

        return value;
    }

    /// <summary>
    /// 检查字符串长度是否超过最大长度，或低于最小长度，否则抛出
    /// <see cref="BeeLengthGreaterDomainException"/>异常
    /// 或<see cref="BeeLengthLessDomainException"/>异常。
    /// </summary>
    /// <param name="value">需检查的字符串。</param>
    /// <param name="valueName">参数名称。</param>
    /// <param name="maxLength">字符串允许的最大长度。</param>
    /// <param name="minLength">字符串要求的最小长度。0表示不限制最小长度</param>
    /// <exception cref="BeeLengthGreaterDomainException"></exception>
    /// <exception cref="BeeLengthLessDomainException"></exception>
    public static string Length(
        string value,
        string valueName,
        int maxLength = int.MaxValue,
        int minLength = 0)
    {
        if (string.IsNullOrEmpty(value))
        {
            // 为null和空时不检查
            return value;
        }

        if (value.Length > maxLength)
        {
            throw new BeeLengthGreaterDomainException(valueName, maxLength);
        }

        if (minLength > 0 && value.Length < minLength)
        {
            throw new BeeLengthLessDomainException(valueName, minLength);
        }

        return value;
    }

    /// <summary>
    /// 检查集合不能为空引用或空集合，否则抛出
    /// <see cref="BeeCollectionNullOrEmptyException"/>异常。
    /// </summary>
    /// <typeparam name="T">集合项的类型。</typeparam>
    /// <param name="list"></param>
    /// <param name="valueName">参数名称。</param>
    /// <exception cref="BeeCollectionNullOrEmptyException"></exception>
    public static void NotNullOrEmpty<T>(
        IReadOnlyList<T> list,
        string valueName)
    {
        if (null == list)
        {
            throw new BeeCollectionNullOrEmptyException(valueName);
        }

        if (!list.Any())
        {
            throw new BeeCollectionNullOrEmptyException(valueName);
        }
    }

    /// <summary>
    /// 检查参数必须小于[或可等于，参数<paramref name="canEqual"/>]指定值，否则抛出<see cref="ArgumentOutOfRangeException"/>异常。
    /// </summary>
    /// <typeparam name="T">参数类型。</typeparam>
    /// <param name="value"></param>
    /// <param name="valueName">参数名称。</param>
    /// <param name="target">要比较的值。</param>
    /// <param name="canEqual">是否可等于。</param>
    /// <exception cref="BeeOutOfRangeException"></exception>
    public static void LessThan<T>(
        T value,
        string valueName,
        T target,
        bool canEqual = false)
        where T : IComparable<T>
    {
        var flag = canEqual ? value.CompareTo(target) <= 0 : value.CompareTo(target) < 0;
        if (!flag)
        {
            throw new BeeOutOfRangeDomainException(valueName, canEqual, false,
                null, target.ToString());
        }
    }

    /// <summary>
    /// 检查参数必须大于[或可等于，参数<paramref name="canEqual"/>]指定值，
    /// 否则抛出<see cref="BeeOutOfRangeException"/>异常。
    /// </summary>
    /// <typeparam name="T">参数类型。</typeparam>
    /// <param name="value"></param>
    /// <param name="parameterName">参数名称。</param>
    /// <param name="target">要比较的值。</param>
    /// <param name="canEqual">是否可等于。</param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static void GreaterThan<T>(T value, string parameterName, T target,
        bool canEqual = true)
        where T : IComparable<T>
    {
        var flag = canEqual ? value.CompareTo(target) >= 0 : value.CompareTo(target) > 0;
        if (!flag)
        {
            throw new BeeOutOfRangeDomainException(parameterName, false, canEqual,
                target.ToString());
        }
    }

    /// <summary>
    /// 检查参数必须在指定范围之间，否则抛出<see cref="BeeOutOfRangeException"/>异常。
    /// </summary>
    /// <typeparam name="T">参数类型。</typeparam>
    /// <param name="value"></param>
    /// <param name="parameterName">参数名称。</param>
    /// <param name="start">比较范围的起始值。</param>
    /// <param name="end">比较范围的结束值。</param>
    /// <param name="startEqual">是否可等于起始值</param>
    /// <param name="endEqual">是否可等于结束值</param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static void Between<T>(T value, string parameterName, T start, T end,
        bool startEqual = false,
        bool endEqual = false)
        where T : IComparable<T>
    {
        var flag = startEqual ? value.CompareTo(start) >= 0 : value.CompareTo(start) > 0;
        if (!flag)
        {
            throw new BeeOutOfRangeDomainException(parameterName, startEqual, false,
                null, start.ToString());
        }

        flag = endEqual ? value.CompareTo(end) <= 0 : value.CompareTo(end) < 0;
        if (!flag)
        {
            throw new BeeOutOfRangeDomainException(parameterName, false, endEqual,
                end.ToString());
        }
    }

    /// <summary>
    /// 检查Guid值不能为Guid.Empty，否则抛出<see cref="BeeEmptyGuidDomainException"/>异常。
    /// </summary>
    /// <param name="value"></param>
    /// <param name="valueName">参数名称。</param>
    /// <exception cref="BeeEmptyGuidDomainException"></exception>
    public static Guid NotEmpty(
        Guid value,
        string valueName)
    {
        if (value == Guid.Empty)
        {
            throw new BeeEmptyGuidDomainException(valueName);
        }

        return value;
    }
}