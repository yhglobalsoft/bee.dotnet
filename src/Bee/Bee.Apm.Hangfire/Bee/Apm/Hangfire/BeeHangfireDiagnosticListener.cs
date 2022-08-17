using Bee.Hangfire.Diagnostics;
using Elastic.Apm;
using Elastic.Apm.Api;

namespace Bee.Apm.Hangfire;

public class BeeHangfireDiagnosticListener : IObserver<KeyValuePair<string, object>>
{
    public void OnCompleted()
    {
    }

    public void OnError(Exception error)
    {
    }


    public void OnNext(KeyValuePair<string, object> value)
    {
        var eventData = (BeeDiagnosticEventData)value.Value;
        ITransaction transaction = null;
        ISpan span = null;


        var tracingContext = BuildDistributedTracingData(eventData.TraceId, eventData.TransactionId, "01");
        if (value.Key == BeeHangfireDiagnosticConsts.BeeHangfireCreating)
        {
            transaction = Agent.Tracer.CurrentTransaction;
            span = transaction?.StartSpan("creating job", BeeApmHangfireDiagnosticConsts.ApmType, BeeApmHangfireDiagnosticConsts.ApmSubType);
            span?.End();
        }

        if (value.Key == BeeHangfireDiagnosticConsts.BeeHangfireCreated)
        {
            transaction = Agent.Tracer.CurrentTransaction;
            span = transaction?.StartSpan("created job", BeeApmHangfireDiagnosticConsts.ApmType, BeeApmHangfireDiagnosticConsts.ApmSubType);
            span?.End();
            transaction?.End();
        }

        if (value.Key == BeeHangfireDiagnosticConsts.BeeHangfirePerforming)
        {
            transaction = Agent.Tracer.StartTransaction(eventData.MethodName, BeeApmHangfireDiagnosticConsts.TransactionType, tracingContext);
            span = transaction?.StartSpan("start job", BeeApmHangfireDiagnosticConsts.ApmType, BeeApmHangfireDiagnosticConsts.ApmSubType);
            transaction?.Custom.Add("job id", eventData.JobId);
            transaction?.Custom.Add("method name", eventData.MethodName);
            transaction?.Custom.Add("method args", eventData.MethodArgs);
            span?.End();
        }

        if (value.Key == BeeHangfireDiagnosticConsts.BeeHangfirePerformed)
        {
            transaction = Agent.Tracer.CurrentTransaction;
            span = transaction?.StartSpan("end job" + BeeApmHangfireDiagnosticConsts.ApmType, BeeApmHangfireDiagnosticConsts.ApmSubType);
            if (eventData.Exception != null)
            {
                transaction?.Custom.Add("exception", eventData.Exception.Message);
                span?.CaptureException(eventData.Exception, eventData.Exception.Message);
            }

            span?.End();
            transaction?.End();
        }
    }

    private DistributedTracingData BuildDistributedTracingData(string traceId, string parentId, string traceFlags) =>
        DistributedTracingData.TryDeserializeFromString(
            "00-" + // version
            (traceId == null ? "" : $"{traceId}") +
            (parentId == null ? "" : $"-{parentId}") +
            (traceFlags == null ? "" : $"-{traceFlags}"));
}