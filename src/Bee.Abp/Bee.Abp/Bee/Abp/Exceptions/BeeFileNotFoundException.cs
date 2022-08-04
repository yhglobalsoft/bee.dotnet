using Microsoft.Extensions.Logging;

namespace Bee.Abp.Exceptions;

public class BeeFileNotFoundException : BeeBusinessException
{
    public BeeFileNotFoundException(
        string filePath,
        string message = null,
        string details = null,
        Exception innerException = null,
        LogLevel logLevel = LogLevel.Warning)
        : base(BeeAbpErrorCodes.FileNotFound, message, details, innerException, logLevel)
    {
        WithData(nameof(filePath), filePath);
    }
}