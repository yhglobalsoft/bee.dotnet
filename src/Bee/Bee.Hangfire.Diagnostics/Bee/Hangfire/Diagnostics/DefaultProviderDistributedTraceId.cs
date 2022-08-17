namespace Bee.Hangfire.Diagnostics;

public class DefaultProviderDistributedTraceId : IProviderDistributedTraceId
{
    public KeyValuePair<string, string>? GetId(string transactionName = "")
    {
        return null;
    }
}