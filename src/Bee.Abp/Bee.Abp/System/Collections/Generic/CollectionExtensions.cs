using Bee;

namespace System.Collections.Generic;

/// <summary>
/// 集合扩展方法
/// </summary>
public static class CollectionExtensions
{
    /// <summary>
    /// 如果条件成立，添加项
    /// </summary>
    public static void AddIf<T>(this ICollection<T> collection, T value, bool flag)
    {
        Guard.NotNull(collection, nameof(collection));
        if (flag)
        {
            collection.Add(value);
        }
    }

    /// <summary>
    /// 如果条件成立，添加项
    /// </summary>
    public static void AddIf<T>(this ICollection<T> collection, T value, Func<bool> func)
    {
        Guard.NotNull(collection, nameof(collection));
        if (func())
        {
            collection.Add(value);
        }
    }

    /// <summary>
    /// 如果不存在，添加项
    /// </summary>
    public static void AddIfNotExist<T>(this ICollection<T> collection, T value, Func<T, bool> existFunc = null)
    {
        Guard.NotNull(collection, nameof(collection));
        var exists = existFunc == null ? collection!.Contains(value) : collection!.Any(existFunc);
        if (!exists)
        {
            collection!.Add(value);
        }
    }

    /// <summary>
    /// 如果不为空，添加项
    /// </summary>
    public static void AddIfNotNull<T>(this ICollection<T> collection, T value) where T : class
    {
        Guard.NotNull(collection, nameof(collection));
        if (value != null)
        {
            collection!.Add(value);
        }
    }

    /// <summary>
    /// 获取对象，不存在对使用委托添加对象
    /// </summary>
    public static T GetOrAdd<T>(this ICollection<T> collection, Func<T, bool> selector, Func<T> factory)
    {
        Guard.NotNull(collection, nameof(collection));
        T item = collection.FirstOrDefault(selector);
        if (item == null)
        {
            item = factory();
            collection.Add(item);
        }

        return item;
    }

    /// <summary>
    /// 交换两项的位置
    /// </summary>
    public static void Swap<T>(this List<T> list, int index1, int index2)
    {
        Guard.Between(index1, nameof(index1), 0, list.Count, true);
        Guard.Between(index2, nameof(index2), 0, list.Count, true);

        if (index1 == index2)
        {
            return;
        }

        T tmp = list[index1];
        list[index1] = list[index2];
        list[index2] = tmp;
    }

    /// <summary>
    /// 判断数字集合是否是连续的
    /// </summary>
    /// <returns>如果参数集合为null或空集合，则返回false</returns>
    public static bool IsContinuous(this List<int> numList)
    {
        if (numList == null || numList.Count == 0)
        {
            return false;
        }

        numList.Sort((x, y) => -x.CompareTo(y));//降序
        bool result = false;
        var totalCount = numList.Count();
        for (int i = 0; i < totalCount - 1; i++)
        {
            result = numList[i] - numList[i + 1] == 1;

            if (!result)
            {
                break;
            }
        }
        return result;
    }
}