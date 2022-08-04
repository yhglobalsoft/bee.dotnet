using System.Runtime.Serialization;
using Microsoft.Extensions.Logging;

namespace Bee.Abp.Exceptions.Domains;

/// <summary>
/// 领域层异常基类
/// </summary>
public class BeeDomainException : BeeBusinessException, IBeeDomainException
{
    public BeeDomainException(SerializationInfo serializationInfo, StreamingContext context)
        : base(serializationInfo, context)
    {
            
    }

    public BeeDomainException(
        string code = null,
        string message = null,
        string details = null,
        Exception innerException = null,
        LogLevel logLevel = LogLevel.Warning)
        : base(code, message, details, innerException, logLevel)
    {
    }
}