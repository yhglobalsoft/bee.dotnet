using Microsoft.Extensions.Logging;

namespace Bee.Abp.Exceptions;

/// <summary>
/// 调用方法时的参数为Null或空或全部为空格.
/// </summary>
public class BeeNullOrWhiteSpaceException : BeeBusinessException
{
    public BeeNullOrWhiteSpaceException(
        string valueName,
        string message = null,
        string details = null,
        Exception innerException = null,
        LogLevel logLevel = LogLevel.Warning)
        : base(BeeAbpErrorCodes.NullOrWhiteSpace, message, details, innerException, logLevel)
    {
        WithData(nameof(valueName), valueName);
    }
}