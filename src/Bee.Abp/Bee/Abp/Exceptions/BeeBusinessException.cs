using System.Runtime.Serialization;
using Microsoft.Extensions.Logging;
using Volo.Abp;
using Volo.Abp.ExceptionHandling;
using Volo.Abp.Logging;

namespace Bee.Abp.Exceptions;

/// <summary>
/// Bee 业务异常类.包含一些Abp框架特性,例如ErrorCode LogLevel 本地化
/// </summary>
[Serializable]
public class BeeBusinessException : Exception, 
    IBeeBusinessException,
    IBusinessException,
    IHasErrorCode,
    IHasErrorDetails, 
    IHasLogLevel
{
    public string Code { get; set; }

    public string Details { get; set; }

    public LogLevel LogLevel { get; set; }

    public BeeBusinessException(
        string code = null, 
        string message = null, 
        string details = null, 
        Exception innerException = null,
        LogLevel logLevel = LogLevel.Warning)
        : base(message, innerException)
    {
        Code = code;
        Details = details;
        LogLevel = logLevel;
    }
        
    /// <summary>
    /// Constructor for serializing.
    /// </summary>
    public BeeBusinessException(SerializationInfo serializationInfo, StreamingContext context)
        : base(serializationInfo, context)
    {

    }

    public BeeBusinessException WithData(string name, object value)
    {
        Data[name] = value;
        return this;
    }

}