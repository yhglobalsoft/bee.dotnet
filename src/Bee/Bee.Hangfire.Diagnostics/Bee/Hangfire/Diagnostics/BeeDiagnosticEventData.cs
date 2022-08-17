namespace Bee.Hangfire.Diagnostics;

/// <summary>
/// 诊断日志数据
/// </summary>
public class BeeDiagnosticEventData
{
    /// <summary>
    /// 任务Id
    /// </summary>
    public string JobId { get; set; }
    
    /// <summary>
    /// 执行方法名称
    /// </summary>
    public string MethodName { get; set; }

    /// <summary>
    /// 执行方法参数
    /// </summary>
    public string MethodArgs { get; set; }
    
    /// <summary>
    /// 异常信息,可空
    /// </summary>
    public Exception Exception { get; set; }
    
    /// <summary>
    /// 分布式追踪Id,可空
    /// </summary>
    public string TraceId { get; set; }
    
    /// <summary>
    /// 分布式追踪事务Id,可空
    /// </summary>
    public string TransactionId { get; set; }
}