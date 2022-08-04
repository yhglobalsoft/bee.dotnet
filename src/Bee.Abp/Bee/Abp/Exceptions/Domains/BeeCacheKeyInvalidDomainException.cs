using Microsoft.Extensions.Logging;

namespace Bee.Abp.Exceptions.Domains;

/// <summary>
/// 领域异常 缓存Key无效异常
/// </summary>
public class BeeCacheKeyInvalidDomainException : BeeDomainException
{
    public BeeCacheKeyInvalidDomainException(
        string message = null,
        string details = null,
        Exception innerException = null,
        LogLevel logLevel = LogLevel.Warning)
        : base(BeeAbpErrorCodes.CacheKeyInvalid, message, details, innerException, logLevel)
    {
    }
}