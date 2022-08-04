using Microsoft.Extensions.Logging;

namespace Bee.Abp.Exceptions;

/// <summary>
/// 调用方法时的参数长度低于限制
/// </summary>
public class BeeLengthLessException :  BeeBusinessException
{
    public BeeLengthLessException(
        string valueName,
        int minLength,
        string message = null,
        string details = null,
        Exception innerException = null,
        LogLevel logLevel = LogLevel.Warning)
        : base(BeeAbpErrorCodes.LengthLess, message, details, innerException, logLevel)
    {
        WithData(nameof(valueName), valueName);
        WithData(nameof(minLength), minLength);
    }
}