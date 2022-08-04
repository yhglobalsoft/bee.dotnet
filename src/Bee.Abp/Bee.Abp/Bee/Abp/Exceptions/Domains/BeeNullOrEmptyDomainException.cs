using Microsoft.Extensions.Logging;

namespace Bee.Abp.Exceptions.Domains;

/// <summary>
/// <see cref="BeeNullOrEmptyException"/>
/// </summary>
public class BeeNullOrEmptyDomainException : BeeNullOrEmptyException, IBeeDomainException
{
    public BeeNullOrEmptyDomainException(
        string valueName,
        string message = null,
        string details = null,
        Exception innerException = null,
        LogLevel logLevel = LogLevel.Warning)
        : base(valueName, message, details, innerException, logLevel)
    {
    }
}