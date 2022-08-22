namespace Bee.Hangfire.Filter;

public class BeeHangfireDiagnosticServerFilter : IServerFilter
{
    private static readonly DiagnosticListener DiagnosticListener = new DiagnosticListener(BeeHangfireDiagnosticConsts.DiagnosticListenerName);
    
    public void OnPerforming(PerformingContext filterContext)
    {
        if (!DiagnosticListener.IsEnabled(BeeHangfireDiagnosticConsts.DiagnosticListenerName)) return;

        var eventData = new BeeDiagnosticEventData()
        {
            JobId = filterContext.BackgroundJob.Id,
            MethodName = filterContext.BackgroundJob.Job.ToString(),
            MethodArgs = JsonConvert.SerializeObject(filterContext.BackgroundJob.Job.Args),
            TraceId = filterContext.GetJobParameter<string>("TraceId"),
            TransactionId = filterContext.GetJobParameter<string>("TransactionId")
        };
        //任务开始执行发送诊断日志
        DiagnosticListener.Write(BeeHangfireDiagnosticConsts.BeeHangfirePerforming, eventData);
    }

    public void OnPerformed(PerformedContext filterContext)
    {
        if (!DiagnosticListener.IsEnabled(BeeHangfireDiagnosticConsts.DiagnosticListenerName)) return;
        var eventData = new BeeDiagnosticEventData()
        {
            JobId = filterContext.BackgroundJob.Id,
            MethodName = filterContext.BackgroundJob.Job.ToString(),
            MethodArgs = JsonConvert.SerializeObject(filterContext.BackgroundJob.Job.Args),
            Exception = filterContext.Exception,
            TraceId = filterContext.GetJobParameter<string>("TraceId"),
            TransactionId = filterContext.GetJobParameter<string>("TransactionId")
        };
        //任务结束发送诊断日志
        DiagnosticListener.Write(BeeHangfireDiagnosticConsts.BeeHangfirePerformed, eventData);
    }
}