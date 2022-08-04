using Microsoft.Extensions.Logging;

namespace Bee.Abp.Exceptions.Domains;

/// <summary>
/// <see cref="BeeNullOrWhiteSpaceException"/>
/// </summary>
public class BeeNullOrWhiteSpaceDomainException :
    BeeNullOrWhiteSpaceException,
    IBeeDomainException
{
    public BeeNullOrWhiteSpaceDomainException(
        string valueName,
        string message = null,
        string details = null,
        Exception innerException = null,
        LogLevel logLevel = LogLevel.Warning)
        : base(valueName, message, details, innerException, logLevel)
    {
    }
}