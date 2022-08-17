using Bee.Hangfire.Diagnostics;
using Elastic.Apm;

namespace Bee.Apm.Hangfire;

public class ApmProviderDistributedTraceId : IProviderDistributedTraceId
{
    public KeyValuePair<string, string>? GetId(string transactionName = "")
    {
        var transaction = Agent.Tracer.CurrentTransaction;
        if (string.IsNullOrWhiteSpace(transactionName))
        {
            transactionName = BeeApmHangfireDiagnosticConsts.DefaultTransactionName;
        }

        if (transaction == null)
        {
            transaction = Agent.Tracer.StartTransaction(transactionName+".All.Transaction", BeeApmHangfireDiagnosticConsts.TransactionType);
        }

        return new KeyValuePair<string, string>(transaction.TraceId, transaction.Id);
    }
}