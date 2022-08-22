namespace Microsoft.Extensions.DependencyInjection;

public static class BeeHangfireServiceCollectionExtensions
{
    public static IServiceCollection AddBeeHangfire(this IServiceCollection services)
    {
        services.AddTransient<DefaultProviderDistributedTraceId>();
        return services;
    }
}