using Microsoft.Extensions.Logging;

namespace Bee.Abp.Exceptions.Domains;

/// <summary>
/// 指定Id的实体未找到异常
/// </summary>
public class BeeEntityNotFoundDomainException : 
    BeeBusinessException, 
    IBeeDomainException
{
    public BeeEntityNotFoundDomainException(
        Type entityType,
        string entityId,
        string message = null,
        string details = null,
        Exception innerException = null,
        LogLevel logLevel = LogLevel.Warning)
        : base(BeeAbpErrorCodes.EntityNotFound, message, details, innerException, logLevel)
    {
        WithData(nameof(entityType), entityType);
        WithData(nameof(entityId), entityId);
    }
}