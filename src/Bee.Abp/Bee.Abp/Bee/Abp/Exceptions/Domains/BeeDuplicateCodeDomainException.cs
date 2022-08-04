using Microsoft.Extensions.Logging;

namespace Bee.Abp.Exceptions.Domains;

/// <summary>
/// 领域异常 实体中Code编码重复时抛出的异常
/// </summary>
public class BeeDuplicateCodeDomainException : BeeDomainException
{
    public BeeDuplicateCodeDomainException(
        string code,
        string message = null,
        string details = null,
        Exception innerException = null,
        LogLevel logLevel = LogLevel.Warning)
        : base(BeeAbpErrorCodes.DuplicateCode, message, details, innerException, logLevel)
    {
        WithData(nameof(code), code);
    }
}