using Microsoft.Extensions.Logging;

namespace Bee.Abp.Exceptions;

/// <summary>
/// 调用方法时的参数类型为集合时 Null或集合明细为空
/// </summary>
public class BeeCollectionNullOrEmptyException : BeeBusinessException
{
    public BeeCollectionNullOrEmptyException(
        string valueName,
        string message = null,
        string details = null,
        Exception innerException = null,
        LogLevel logLevel = LogLevel.Warning)
        : base(BeeAbpErrorCodes.CollectionNullOrEmpty, message, details, innerException, logLevel)
    {
        WithData(nameof(valueName), valueName);
    }
}