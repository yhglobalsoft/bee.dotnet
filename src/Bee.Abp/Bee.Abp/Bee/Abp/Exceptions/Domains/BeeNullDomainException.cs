using Microsoft.Extensions.Logging;

namespace Bee.Abp.Exceptions.Domains;

/// <summary>
/// <see cref="BeeNullException"/>
/// </summary>
public class BeeNullDomainException : BeeNullException, IBeeDomainException
{
    public BeeNullDomainException(
        string valueName,
        string message = null,
        string details = null,
        Exception innerException = null,
        LogLevel logLevel = LogLevel.Warning)
        : base(valueName, message, details, innerException, logLevel)
    {
    }
}