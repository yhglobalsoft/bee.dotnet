using Microsoft.Extensions.Logging;

namespace Bee.Abp.Exceptions;

/// <summary>
/// 调用方法时的参数无效
/// </summary>
public class BeeInvalidException : BeeBusinessException
{
    public BeeInvalidException(
        string valueName,
        string message = null,
        string details = null,
        Exception innerException = null,
        LogLevel logLevel = LogLevel.Warning)
        : base(BeeAbpErrorCodes.Invalid, message, details, innerException, logLevel)
    {
        WithData(nameof(valueName), valueName);
    }
}