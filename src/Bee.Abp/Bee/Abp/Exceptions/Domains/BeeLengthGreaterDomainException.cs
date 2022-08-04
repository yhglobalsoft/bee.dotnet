using Microsoft.Extensions.Logging;

namespace Bee.Abp.Exceptions.Domains;

/// <summary>
/// <see cref="BeeLengthGreaterException"/>
/// </summary>
public class BeeLengthGreaterDomainException : BeeLengthGreaterException, IBeeDomainException
{
    public BeeLengthGreaterDomainException(
        string valueName,
        int maxLength,
        string message = null,
        string details = null,
        Exception innerException = null,
        LogLevel logLevel = LogLevel.Warning)
        : base(valueName, maxLength, message, details, innerException, logLevel)
    {
    }
}