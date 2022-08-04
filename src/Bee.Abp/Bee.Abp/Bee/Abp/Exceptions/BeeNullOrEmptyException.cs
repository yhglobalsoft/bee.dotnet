using Microsoft.Extensions.Logging;

namespace Bee.Abp.Exceptions;

/// <summary>
/// 调用方法时的参数为Null或空.
/// </summary>
public class BeeNullOrEmptyException : BeeBusinessException
{
    public BeeNullOrEmptyException(
        string valueName,
        string message = null,
        string details = null,
        Exception innerException = null,
        LogLevel logLevel = LogLevel.Warning)
        : base(BeeAbpErrorCodes.NullOrEmpty, message, details, innerException, logLevel)
    {
        WithData(nameof(valueName), valueName);
    }
}