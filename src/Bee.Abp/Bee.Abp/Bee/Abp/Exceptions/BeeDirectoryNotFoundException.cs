using Microsoft.Extensions.Logging;

namespace Bee.Abp.Exceptions;

/// <summary>
/// 目录未找到异常
/// </summary>
public class BeeDirectoryNotFoundException : BeeBusinessException
{
    public BeeDirectoryNotFoundException(
        string directoryPath,
        string message = null,
        string details = null,
        Exception innerException = null,
        LogLevel logLevel = LogLevel.Warning)
        : base(BeeAbpErrorCodes.DirectoryNotFound, message, details, innerException, logLevel)
    {
        WithData(nameof(directoryPath), directoryPath);
    }
}