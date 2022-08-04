using Microsoft.Extensions.Logging;

namespace Bee.Abp.Exceptions.Domains;

/// <summary>
/// <see cref="BeeLengthLessException"/>
/// </summary>
public class BeeLengthLessDomainException : BeeLengthLessException, IBeeDomainException
{
    public BeeLengthLessDomainException(
        string valueName,
        int minLength,
        string message = null,
        string details = null,
        Exception innerException = null,
        LogLevel logLevel = LogLevel.Warning)
        : base(valueName, minLength, message, details, innerException, logLevel)
    {
    }
}