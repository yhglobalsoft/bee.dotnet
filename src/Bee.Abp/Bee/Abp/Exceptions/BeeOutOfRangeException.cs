using Microsoft.Extensions.Logging;

namespace Bee.Abp.Exceptions;

/// <summary>
/// 调用方法时的参数超出范围异常
/// </summary>
public class BeeOutOfRangeException : BeeBusinessException
{
    public BeeOutOfRangeException(
        string valueName,
        bool startEqual,
        bool endEqual,
        string maxValue = null,
        string minValue = null,
        string message = null,
        string details = null,
        Exception innerException = null,
        LogLevel logLevel = LogLevel.Warning)
        : base(string.Empty, message, details, innerException, logLevel)
    {
        WithData(nameof(valueName), valueName);

        if (minValue.IsNullOrWhiteSpace() && maxValue.IsNullOrWhiteSpace())
        {
            // 没有最小值和最大值
            Code = BeeAbpErrorCodes.OutOfRange;
        }
        else if (!string.IsNullOrWhiteSpace(minValue))
        {
            // 有最小值 判断左右是否闭合
            Code = startEqual ? BeeAbpErrorCodes.LessEqual : BeeAbpErrorCodes.Less;
            WithData(nameof(minValue), minValue!);
        }
        else
        {
            // 有最大值 判断左右是否闭合
            Code = endEqual ? BeeAbpErrorCodes.GreaterEqual : BeeAbpErrorCodes.Greater;
            WithData(nameof(maxValue), maxValue!);
        }
    }
}