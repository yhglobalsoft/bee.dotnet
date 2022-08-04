﻿using System.ComponentModel;
using System.Text;
using Volo.Abp;
using Bee;
using Bee.Filter;


namespace System.Collections.Generic;

/// <summary>
/// Enumerable集合扩展方法
/// </summary>
public static class EnumerableExtensions
{
    /// <summary>断言集合中的元素符合指定表达式，否则抛出异常。</summary>
    /// <typeparam name="T">集合项类型</typeparam>
    /// <param name="source">源集合</param>
    /// <param name="predicate">元素判断表达式</param>
    /// <param name="errorSelector">异常选择器</param>
    /// <returns>筛选过的集合</returns>
    public static IEnumerable<T> Assert<T>(this IEnumerable<T> source, Func<T, bool> predicate,
        Func<T, Exception> errorSelector = null)
    {
        foreach (var item in source)
        {
            var success = predicate(item);
            if (!success)
            {
                throw errorSelector?.Invoke(item) ?? new BusinessException(code: "Bee.Abp:0001");
            }

            yield return item;
        }
    }

    /// <summary>
    /// 将集合展开并分别转换成字符串，再以指定的分隔符衔接，拼成一个字符串返回。默认分隔符为逗号。
    /// </summary>
    /// <param name="collection">要处理的集合</param>
    /// <param name="separator">分隔符，默认为逗号</param>
    /// <returns>拼接后的字符串</returns>
    public static string ExpandAndToString<T>(this IEnumerable<T> collection, string separator = ",")
    {
        return collection.ExpandAndToString(item => item?.ToString() ?? string.Empty, separator);
    }

    /// <summary>
    /// 循环集合的每一项，调用委托生成字符串，返回合并后的字符串。默认分隔符为逗号
    /// </summary>
    /// <param name="collection">待处理的集合</param>
    /// <param name="itemFormatFunc">单个集合项的转换委托</param>
    /// <param name="separator">分隔符，默认为逗号</param>
    /// <typeparam name="T">泛型类型</typeparam>
    /// <returns></returns>
    public static string ExpandAndToString<T>(
        this IEnumerable<T> collection,
        Func<T, string> itemFormatFunc,
        string separator = ",")
    {
        collection = collection as IList<T> ?? collection.ToList();
        Guard.NotNull(itemFormatFunc, nameof(itemFormatFunc));

        if (!collection.Any())
        {
            return string.Empty;
        }

        var sb = new StringBuilder();
        var i = 0;
        var count = collection.Count();
        foreach (var item in collection)
        {
            if (i == count - 1)
            {
                sb.Append(itemFormatFunc(item));
            }
            else
            {
                sb.Append(itemFormatFunc(item) + separator);
            }

            i++;
        }

        return sb.ToString();
    }

    /// <summary>
    /// 集合是否为空
    /// </summary>
    /// <param name="collection"> 要处理的集合 </param>
    /// <typeparam name="T"> 动态类型 </typeparam>
    /// <returns> 为空返回True，不为空返回False </returns>
    public static bool IsEmpty<T>(this IEnumerable<T> collection)
    {
        collection = collection as IList<T> ?? collection.ToList();
        return !collection.Any();
    }

    /// <summary>
    /// 根据第三方条件是否为真来决定是否执行指定条件的查询
    /// </summary>
    /// <param name="source"> 要查询的源 </param>
    /// <param name="predicate"> 查询条件 </param>
    /// <param name="condition"> 第三方条件 </param>
    /// <typeparam name="T"> 动态类型 </typeparam>
    /// <returns> 查询的结果 </returns>
    public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, Func<T, bool> predicate, bool condition)
    {
        Guard.NotNull(predicate, nameof(predicate));
        source = source as IList<T> ?? source.ToList();

        return condition ? source.Where(predicate) : source;
    }

    /// <summary>
    /// 将字符串集合按指定前缀排序
    /// </summary>
    public static IEnumerable<T> OrderByPrefixes<T>(this IEnumerable<T> source, Func<T, string> keySelector,
        params string[] prefixes)
    {
        var all = source.OrderBy(keySelector).ToList();
        var result = new List<T>();
        foreach (var prefix in prefixes)
        {
            var tmpList = all.Where(m => keySelector(m).StartsWith(prefix)).OrderBy(keySelector).ToList();
            all = all.Except(tmpList).ToList();
            result.AddRange(tmpList);
        }

        result.AddRange(all);
        return result;
    }

    /// <summary>
    /// 根据指定条件返回集合中不重复的元素
    /// </summary>
    /// <typeparam name="T">动态类型</typeparam>
    /// <typeparam name="TKey">动态筛选条件类型</typeparam>
    /// <param name="source">要操作的源</param>
    /// <param name="keySelector">重复数据筛选条件</param>
    /// <returns>不重复元素的集合</returns>
    public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> keySelector)
    {
        Guard.NotNull(keySelector, nameof(keySelector));
        source = source as IList<T> ?? source.ToList();

        return source.GroupBy(keySelector).Select(group => group.First());
    }

    /// <summary>
    /// 把<see cref="IEnumerable{T}"/>集合按指定字段与排序方式进行排序
    /// </summary>
    /// <typeparam name="T">集合项类型</typeparam>
    /// <param name="source">要排序的数据集</param>
    /// <param name="propertyName">排序属性名</param>
    /// <param name="sortDirection">排序方向</param>
    /// <returns>排序后的数据集</returns>
    public static IOrderedEnumerable<T> OrderBy<T>(
        this IEnumerable<T> source,
        string propertyName,
        ListSortDirection sortDirection = ListSortDirection.Ascending)
    {
        Guard.NotNull(propertyName, nameof(propertyName));

        return CollectionPropertySorter<T>.OrderBy(source, propertyName!, sortDirection);
    }

    /// <summary>
    /// 把<see cref="IEnumerable{T}"/>集合按指定字段排序条件进行排序
    /// </summary>
    /// <typeparam name="T">动态类型</typeparam>
    /// <param name="source">要排序的数据集</param>
    /// <param name="sortCondition">列表字段排序条件</param>
    /// <returns></returns>
    public static IOrderedEnumerable<T> OrderBy<T>(this IEnumerable<T> source, SortCondition sortCondition)
    {
        Guard.NotNull(sortCondition, nameof(sortCondition));

        return source.OrderBy(sortCondition.SortField, sortCondition.ListSortDirection);
    }

    /// <summary>
    /// 把<see cref="IEnumerable{T}"/>集合按指定字段排序条件进行排序
    /// </summary>
    /// <typeparam name="T">动态类型</typeparam>
    /// <param name="source">要排序的数据集</param>
    /// <param name="sortCondition">列表字段排序条件</param>
    /// <returns></returns>
    public static IOrderedEnumerable<T> OrderBy<T>(this IEnumerable<T> source, SortCondition<T> sortCondition)
    {
        Guard.NotNull(sortCondition, nameof(sortCondition));

        return source.OrderBy(sortCondition.SortField, sortCondition.ListSortDirection);
    }

    /// <summary>
    /// 把<see cref="IOrderedQueryable{T}"/>集合继续按指定字段排序方式进行排序
    /// </summary>
    /// <typeparam name="T">动态类型</typeparam>
    /// <param name="source">要排序的数据集</param>
    /// <param name="propertyName">排序属性名</param>
    /// <param name="sortDirection">排序方向</param>
    /// <returns></returns>
    public static IOrderedEnumerable<T> ThenBy<T>(
        this IOrderedEnumerable<T> source,
        string propertyName,
        ListSortDirection sortDirection = ListSortDirection.Ascending)
    {
        Guard.NotNull(propertyName, nameof(propertyName));

        return CollectionPropertySorter<T>.ThenBy(source, propertyName!, sortDirection);
    }

    /// <summary>
    /// 把<see cref="IOrderedEnumerable{T}"/>集合继续指定字段排序方式进行排序
    /// </summary>
    /// <typeparam name="T">动态类型</typeparam>
    /// <param name="source">要排序的数据集</param>
    /// <param name="sortCondition">列表字段排序条件</param>
    /// <returns></returns>
    public static IOrderedEnumerable<T> ThenBy<T>(this IOrderedEnumerable<T> source, SortCondition sortCondition)
    {
        Guard.NotNull(sortCondition, nameof(sortCondition));

        return source.ThenBy(sortCondition.SortField, sortCondition.ListSortDirection);
    }

    #region Internal

    internal static int? TryGetCollectionCount<T>(this IEnumerable<T> source)
    {
        switch (source)
        {
            case null:
                throw new ArgumentNullException(nameof(source));
            case ICollection<T> collection:
                return collection.Count;
            case IReadOnlyCollection<T> collection:
                return collection.Count;
            default:
                return null;
        }
    }

    static int CountUpTo<T>(this IEnumerable<T> source, int max)
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }
        if (max < 0)
        {
            throw new BusinessException(code: "Bee.Abp:0002").WithData("paramName", nameof(max));
        }


        var count = 0;

        using (var e = source.GetEnumerator())
        {
            while (count < max && e.MoveNext())
            {
                count++;
            }
        }

        return count;
    }

    #endregion
}