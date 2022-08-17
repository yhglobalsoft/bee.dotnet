namespace Bee.Hangfire.Diagnostics;

/// <summary>
/// 诊断日志常量
/// </summary>
public static class BeeHangfireDiagnosticConsts
{
    /// <summary>
    /// 诊断日志名称
    /// </summary>
    public const string DiagnosticListenerName = "BeeHangfireDiagnosticListener";

    /// <summary>
    /// 开始创建Job
    /// </summary>
    public const string BeeHangfireCreating = "Bee.Hangfire.Creating";

    /// <summary>
    /// 创建Job完成
    /// </summary>
    public const string BeeHangfireCreated = "Bee.Hangfire.Created";
    
    /// <summary>
    /// 开始执行Job
    /// </summary>
    public const string BeeHangfirePerforming = "Bee.Hangfire.Performing";

    /// <summary>
    /// Job执行完成
    /// </summary>
    public const string BeeHangfirePerformed = "Bee.Hangfire.Performed";
}