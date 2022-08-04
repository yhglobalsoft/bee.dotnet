using System.Collections.Concurrent;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Bee;

namespace System;

/// <summary>
/// 基类型<see cref="Object"/>扩展辅助操作类
/// </summary>
public static class ObjectExtensions
{
    /// <summary>
    /// 把对象类型转换为指定类型
    /// </summary>
    /// <param name="value"></param>
    /// <param name="conversionType"></param>
    /// <returns></returns>
    public static object CastTo(this object value, Type conversionType)
    {
        if (value == null)
        {
            return null;
        }

        if (conversionType.IsNullableType())
        {
            conversionType = conversionType.GetUnNullableType();
        }

        if (conversionType.IsEnum)
        {
            return Enum.Parse(conversionType, value.ToString());
        }

        if (conversionType == typeof(Guid))
        {
            return Guid.Parse(value.ToString());
        }

        return Convert.ChangeType(value, conversionType);
    }

    /// <summary>
    /// 把对象类型转化为指定类型
    /// </summary>
    /// <typeparam name="T">动态类型</typeparam>
    /// <param name="value">要转化的源对象</param>
    /// <returns>转化后的指定类型的对象，转化失败引发异常。</returns>
    public static T CastTo<T>(this object value)
    {
        if (value == null && default(T) == null)
        {
            return default;
        }

        if (value!.GetType() == typeof(T))
        {
            return (T) value;
        }

        var result = CastTo(value, typeof(T));
        return (T) result!;
    }

    /// <summary>
    /// 把对象类型转化为指定类型，转化失败时返回指定的默认值
    /// </summary>
    /// <typeparam name="T"> 动态类型 </typeparam>
    /// <param name="value"> 要转化的源对象 </param>
    /// <param name="defaultValue"> 转化失败返回的指定默认值 </param>
    /// <returns> 转化后的指定类型对象，转化失败时返回指定的默认值 </returns>
    public static T CastTo<T>(this object value, T defaultValue)
    {
        try
        {
            return CastTo<T>(value);
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }

    /// <summary>
    /// 判断当前值是否介于指定范围内
    /// </summary>
    /// <typeparam name="T">动态类型</typeparam>
    /// <param name="value">动态类型对象</param>
    /// <param name="start">范围起点</param>
    /// <param name="end">范围终点</param>
    /// <param name="leftEqual">是否可等于上限（默认等于）</param>
    /// <param name="rightEqual">是否可等于下限（默认等于）</param>
    /// <returns>是否介于</returns>
    public static bool IsBetween<T>(this IComparable<T> value, T start, T end, bool leftEqual = true,
        bool rightEqual = true) where T : IComparable
    {
        var flag = leftEqual ? value.CompareTo(start) >= 0 : value.CompareTo(start) > 0;
        return flag && (rightEqual ? value.CompareTo(end) <= 0 : value.CompareTo(end) < 0);
    }

    /// <summary>
    /// 判断当前值是否介于指定范围内
    /// </summary>
    /// <typeparam name="T">动态类型</typeparam>
    /// <param name="value">动态类型对象</param>
    /// <param name="min">范围小值</param>
    /// <param name="max">范围大值</param>
    /// <param name="minEqual">是否可等于小值（默认等于）</param>
    /// <param name="maxEqual">是否可等于大值（默认等于）</param>
    public static bool IsInRange<T>(this IComparable<T> value, T min, T max, bool minEqual = true,
        bool maxEqual = true) where T : IComparable
    {
        var flag = minEqual ? value.CompareTo(min) >= 0 : value.CompareTo(min) > 0;
        return flag && (maxEqual ? value.CompareTo(max) <= 0 : value.CompareTo(max) < 0);
    }

    /// <summary>
    /// 将对象[主要是匿名对象]转换为dynamic
    /// </summary>
    public static dynamic ToDynamic(this object value)
    {
        IDictionary<string, object> expando = new ExpandoObject();
        var type = value.GetType();
        var properties = TypeDescriptor.GetProperties(type);
        foreach (PropertyDescriptor property in properties)
        {
            var val = property.GetValue(value);
            if (property.PropertyType.FullName != null &&
                property.PropertyType.FullName.StartsWith("<>f__AnonymousType"))
            {
                expando.Add(property.Name, val!.ToDynamic());
            }
            else
            {
                expando.Add(property.Name, val);
            }
        }

        return (ExpandoObject) expando;
    }

    private static readonly ConcurrentDictionary<Type, ConcurrentDictionary<PropertyInfo, ValidationAttribute[]>>
        ValidationDict
            = new ConcurrentDictionary<Type, ConcurrentDictionary<PropertyInfo, ValidationAttribute[]>>();

    /// <summary>
    /// 验证对象的<see cref="ValidationAttribute"/>特性
    /// </summary>
    public static void Validate(this object obj)
    {
        Guard.NotNull(obj, nameof(obj));
        var type = obj.GetType();
        if (!ValidationDict.TryGetValue(type, out var dict))
        {
            var properties = type.GetProperties();
            dict = new ConcurrentDictionary<PropertyInfo, ValidationAttribute[]>();
            if (properties.Length == 0)
            {
                ValidationDict[type] = dict;
                return;
            }

            foreach (var property in properties)
            {
                dict[property] = null;
            }

            ValidationDict[type] = dict;
        }

        foreach (var property in dict.Keys)
        {
            if (!dict.TryGetValue(property, out var attributes) || attributes == null)
            {
                attributes = property.GetAttributes<ValidationAttribute>();
                dict[property] = attributes;
            }

            if (attributes.Length == 0)
            {
                continue;
            }

            var value = property.GetValue(obj);
            foreach (var attribute in attributes)
            {
                attribute.Validate(value, property.Name);
            }
        }
    }

    /// <summary>
    /// 将对象转换为JSON字符串
    /// </summary> 
    /// <param name="obj">要转换的对象</param>
    /// <param name="camelCase">是否小写名称</param>
    /// <param name="indented">是否换行缩进</param>
    /// <param name="ignore">是否忽略Null值</param>
    /// <returns></returns>
    public static string ToJsonString(this object obj, bool camelCase = false, bool indented = false, bool ignore = false)
    {
        var settings = new JsonSerializerSettings()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        if (camelCase)
        {
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }

        if (indented)
        {
            settings.Formatting = Formatting.Indented;
        }

        if (ignore)
        {
            settings.NullValueHandling = NullValueHandling.Ignore;
        }

        return JsonConvert.SerializeObject(obj, settings);
    }

    /// <summary>
    /// 将对象转换为JSON字符串
    /// </summary>
    /// <param name="obj">要转换的对象</param>
    /// <param name="formatting">转换格式</param>
    public static string ToJson(this object obj, Formatting formatting = Formatting.None)
    {
        return JsonConvert.SerializeObject(obj, formatting);
    }
}