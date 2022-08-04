using System.Runtime.Serialization;
using Microsoft.Extensions.Logging;

namespace Bee.Abp.Exceptions.Applications;

/// <summary>
/// 应用层异常基类
/// </summary>
public class BeeApplicationException : BeeBusinessException, IBeeApplicationException
{
    public BeeApplicationException(SerializationInfo serializationInfo, StreamingContext context)
        : base(serializationInfo, context)
    {
    }

    public BeeApplicationException(
        string code = null,
        string message = null,
        string details = null,
        Exception innerException = null,
        LogLevel logLevel = LogLevel.Warning)
        : base(code, message, details, innerException, logLevel)
    {
    }
}