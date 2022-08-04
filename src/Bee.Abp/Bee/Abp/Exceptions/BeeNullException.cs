using Microsoft.Extensions.Logging;

namespace Bee.Abp.Exceptions;

/// <summary>
/// 调用方法时的参数为Null
/// </summary>
public class BeeNullException : BeeBusinessException
{
    public BeeNullException(
        string valueName,
        string message = null,
        string details = null,
        Exception innerException = null,
        LogLevel logLevel = LogLevel.Warning)
        : base(BeeAbpErrorCodes.Null, message, details, innerException, logLevel)
    {
        WithData(nameof(valueName), valueName);
    }
}