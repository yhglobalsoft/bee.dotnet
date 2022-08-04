using Microsoft.Extensions.Logging;

namespace Bee.Abp.Exceptions.Domains;

/// <summary>
/// <see cref="BeeInvalidException"/>
/// </summary>
public class BeeInvalidDomainException : BeeInvalidException, IBeeDomainException
{
    public BeeInvalidDomainException(
        string valueName,
        string message = null,
        string details = null,
        Exception innerException = null,
        LogLevel logLevel = LogLevel.Warning)
        : base(valueName, message, details, innerException, logLevel)
    {
    }
}