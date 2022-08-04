using Microsoft.Extensions.Logging;

namespace Bee.Abp.Exceptions;

/// <summary>
/// Guid值为Guid.Empty时
/// </summary>
public class BeeEmptyGuidException : BeeBusinessException
{
    public BeeEmptyGuidException(
        string valueName,
        string message = null,
        string details = null,
        Exception innerException = null,
        LogLevel logLevel = LogLevel.Warning)
        : base(BeeAbpErrorCodes.EmptyGuid, message, details, innerException, logLevel)
    {
        WithData(nameof(valueName), valueName);
    }
}