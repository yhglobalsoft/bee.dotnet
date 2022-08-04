using System.ComponentModel;
using Bee;
using Bee.Filter;

namespace System.Linq;

/// <summary>
/// IQueryable集合扩展方法
/// </summary>
public static class QueryableExtensions
{

    /// <summary>
    /// 把<see cref="IQueryable{T}"/>集合按指定字段与排序方式进行排序
    /// </summary>
    /// <param name="source">要排序的数据集</param>
    /// <param name="propertyName">排序属性名</param>
    /// <param name="sortDirection">排序方向</param>
    /// <typeparam name="T">动态类型</typeparam>
    /// <returns>排序后的数据集</returns>
    public static IOrderedQueryable<T> OrderBy<T>(
        this IQueryable<T> source,
        string propertyName,
        ListSortDirection sortDirection = ListSortDirection.Ascending)
    {
        Guard.NotNull(source, nameof(source));
        Guard.NotNull(propertyName, nameof(propertyName));

        return CollectionPropertySorter<T>.OrderBy(source, propertyName, sortDirection);
    }

    /// <summary>
    /// 把<see cref="IQueryable{T}"/>集合按指定字段排序条件进行排序
    /// </summary>
    /// <typeparam name="T">动态类型</typeparam>
    /// <param name="source">要排序的数据集</param>
    /// <param name="sortCondition">列表字段排序条件</param>
    /// <returns></returns>
    public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, SortCondition sortCondition)
    {
        Guard.NotNull(source, nameof(source));
        Guard.NotNull(sortCondition, nameof(sortCondition));

        return source.OrderBy(sortCondition!.SortField!, sortCondition.ListSortDirection);
    }

    /// <summary>
    /// 把<see cref="IQueryable{T}"/>集合按指定字段排序条件进行排序
    /// </summary>
    /// <typeparam name="T">动态类型</typeparam>
    /// <param name="source">要排序的数据集</param>
    /// <param name="sortCondition">列表字段排序条件</param>
    /// <returns></returns>
    public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, SortCondition<T> sortCondition)
    {
        Guard.NotNull(source, nameof(source));
        Guard.NotNull(sortCondition, nameof(sortCondition));

        return source.OrderBy(sortCondition!.SortField!, sortCondition.ListSortDirection);
    }

    /// <summary>
    /// 把<see cref="IOrderedQueryable{T}"/>集合继续按指定字段排序方式进行排序
    /// </summary>
    /// <typeparam name="T">动态类型</typeparam>
    /// <param name="source">要排序的数据集</param>
    /// <param name="propertyName">排序属性名</param>
    /// <param name="sortDirection">排序方向</param>
    /// <returns></returns>
    public static IOrderedQueryable<T> ThenBy<T>(
        this IOrderedQueryable<T> source,
        string propertyName,
        ListSortDirection sortDirection = ListSortDirection.Ascending)
    {
        Guard.NotNull(source, nameof(source));
        Guard.NotNull(propertyName, nameof(propertyName));

        return CollectionPropertySorter<T>.ThenBy(source, propertyName!, sortDirection);
    }

    /// <summary>
    /// 把<see cref="IOrderedQueryable{T}"/>集合继续指定字段排序方式进行排序
    /// </summary>
    /// <typeparam name="T">动态类型</typeparam>
    /// <param name="source">要排序的数据集</param>
    /// <param name="sortCondition">列表字段排序条件</param>
    /// <returns></returns>
    public static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> source, SortCondition sortCondition)
    {
        Guard.NotNull(source, nameof(source));
        Guard.NotNull(sortCondition, nameof(sortCondition));

        return source.ThenBy(sortCondition!.SortField!, sortCondition.ListSortDirection);
    }
}