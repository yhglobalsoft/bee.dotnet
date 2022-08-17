using System.Diagnostics;
using Bee.Hangfire.Diagnostics;
using Hangfire.Client;
using Newtonsoft.Json;

namespace Bee.Hangfire.Filter;

public class BeeHangfireDiagnosticClientFilter : IClientFilter
{
    private static readonly DiagnosticListener DiagnosticListener = new DiagnosticListener(BeeHangfireDiagnosticConsts.DiagnosticListenerName);
    private readonly IProviderDistributedTraceId _providerDistributedTraceId;

    public BeeHangfireDiagnosticClientFilter(IProviderDistributedTraceId providerDistributedTraceId)
    {
        _providerDistributedTraceId = providerDistributedTraceId;
    }

    public void OnCreating(CreatingContext filterContext)
    {
        if (!DiagnosticListener.IsEnabled(BeeHangfireDiagnosticConsts.DiagnosticListenerName)) return;
        var id = _providerDistributedTraceId.GetId(filterContext.Job.ToString());
        
        if (id != null)
        {
            filterContext.SetJobParameter("TraceId", id?.Key);
            filterContext.SetJobParameter("TransactionId", id?.Value);
        }
        
      
        var eventData = new BeeDiagnosticEventData()
        {
            MethodName = filterContext.Job.ToString(),
            MethodArgs = JsonConvert.SerializeObject(filterContext.Job.Args),
            TraceId = id?.Key,
            TransactionId = id?.Value,
        };
        
        
        //任务创建发送诊断日志
        DiagnosticListener.Write(BeeHangfireDiagnosticConsts.BeeHangfireCreating, eventData);
    }

    public void OnCreated(CreatedContext filterContext)
    {
        if (!DiagnosticListener.IsEnabled(BeeHangfireDiagnosticConsts.DiagnosticListenerName)) return;
        var id = _providerDistributedTraceId.GetId();
        
        var eventData = new BeeDiagnosticEventData()
        {
            MethodName = filterContext.Job.ToString(),
            MethodArgs = JsonConvert.SerializeObject(filterContext.Job.Args),
            TraceId = id?.Key,
            TransactionId = id?.Value,
        };


        //任务创建发送诊断日志
        DiagnosticListener.Write(BeeHangfireDiagnosticConsts.BeeHangfireCreated, eventData);
    }
}