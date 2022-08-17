namespace Bee.Hangfire.Diagnostics;

public interface IProviderDistributedTraceId
{
    KeyValuePair<string, string>? GetId(string transactionName = "");
}