using Microsoft.Extensions.Logging;

namespace Bee.Abp.Exceptions;

/// <summary>
/// 调用方法时的参数长度超出限制
/// </summary>
public class BeeLengthGreaterException : BeeBusinessException
{
    public BeeLengthGreaterException(
        string valueName,
        int maxLength,
        string message = null,
        string details = null,
        Exception innerException = null,
        LogLevel logLevel = LogLevel.Warning)
        : base(BeeAbpErrorCodes.LengthGreater, message, details, innerException, logLevel)
    {
        WithData(nameof(valueName), valueName);
        WithData(nameof(maxLength), maxLength);
    }
}