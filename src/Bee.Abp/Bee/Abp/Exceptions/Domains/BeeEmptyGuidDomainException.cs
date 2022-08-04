using Microsoft.Extensions.Logging;

namespace Bee.Abp.Exceptions.Domains;

/// <summary>
/// <see cref="BeeEmptyGuidException"/>
/// </summary>
public class BeeEmptyGuidDomainException : BeeEmptyGuidException
{
    public BeeEmptyGuidDomainException(
        string valueName,
        string message = null,
        string details = null,
        Exception innerException = null,
        LogLevel logLevel = LogLevel.Warning)
        : base(valueName, message, details, innerException, logLevel)
    {
    }
}