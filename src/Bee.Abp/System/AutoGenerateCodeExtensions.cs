﻿using System.Linq;
using Volo.Abp;

namespace System;

/// <summary>
/// 生成自增长编码
/// </summary>
public static class AutoGenerateCodeExtensions
{
    public const int AutoGenerateCodeLength = 5;
    /// <summary>
    /// Calculates next code for given code.
    /// Example: if code = "00019.00055.00001" returns "00019.00055.00002".
    /// </summary>
    /// <param name="code">The code.</param>
    public static string CalculateNextCode(string code)
    {
        if (code.IsNullOrEmpty())
        {
            throw new BusinessException(code: "Bee.Abp:0004").WithData("paramName", nameof(code));
        }

        var parentCode = GetParentCode(code);
        var lastUnitCode = GetLastUnitCode(code);

        return AppendCode(parentCode, CreateCode(Convert.ToInt32(lastUnitCode) + 1));
    }


    /// <summary>
    /// Gets the last unit code.
    /// Example: if code = "00019.00055.00001" returns "00001".
    /// </summary>
    /// <param name="code">The code.</param>
    public static string GetLastUnitCode(string code)
    {
        if (code.IsNullOrEmpty())
        {
            throw new BusinessException(code: "Bee.Abp:0004").WithData("paramName", nameof(code));
        }

        var splittedCode = code.Split('.');
        return splittedCode[splittedCode.Length - 1];
    }

    /// <summary>
    /// Gets parent code.
    /// Example: if code = "00019.00055.00001" returns "00019.00055".
    /// </summary>
    /// <param name="code">The code.</param>
    public static string GetParentCode(string code)
    {
        if (code.IsNullOrEmpty())
        {
            throw new BusinessException(code: "Bee.Abp:0004").WithData("paramName", nameof(code));
        }

        var splittedCode = code.Split('.');
        if (splittedCode.Length == 1)
        {
            return null;
        }

        return splittedCode.Take(splittedCode.Length - 1).JoinAsString(".");
    }


    /// <summary>
    /// Creates code for given numbers.
    /// Example: if numbers are 4,2 then returns "00004.00002";
    /// </summary>
    /// <param name="numbers">Numbers</param>
    public static string CreateCode(params int[] numbers)
    {
        if (numbers == null)
        {
            return null;
        }

        return numbers.Select(number => number.ToString(new string('0', AutoGenerateCodeLength))).JoinAsString(".");
    }

    /// <summary>
    /// Appends a child code to a parent code.
    /// Example: if parentCode = "00001", childCode = "00042" then returns "00001.00042".
    /// </summary>
    /// <param name="parentCode">Parent code. Can be null or empty if parent is a root.</param>
    /// <param name="childCode">Child code.</param>
    public static string AppendCode(string parentCode, string childCode)
    {
        if (childCode.IsNullOrEmpty())
        {
            throw new BusinessException(code: "Bee.Abp:0005").WithData("paramName", nameof(childCode));
        }

        if (parentCode.IsNullOrEmpty())
        {
            return childCode;
        }

        return parentCode + "." + childCode;
    }
}