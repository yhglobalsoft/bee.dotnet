using Microsoft.Extensions.Logging;

namespace Bee.Abp.Exceptions.Domains;

/// <summary>
/// <see cref="BeeOutOfRangeException"/>
/// </summary>
public class BeeOutOfRangeDomainException : BeeOutOfRangeException
{
    public BeeOutOfRangeDomainException(
        string valueName,
        bool startEqual, 
        bool endEqual, 
        string maxValue = null, 
        string minValue = null, 
        string message = null,
        string details = null, 
        Exception innerException = null, 
        LogLevel logLevel = LogLevel.Warning) 
        : base(valueName, startEqual, endEqual, maxValue, minValue, message, details, 
            innerException, logLevel)
    {
    }
}