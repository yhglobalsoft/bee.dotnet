namespace Microsoft.Extensions.DependencyInjection;

public static class BeeApmHangfireServiceCollectionExtensions
{
    /// <summary>
    /// 开启 Hangfire APM 监控
    /// </summary>
    public static IServiceCollection AddBeeApmHangfire(this IServiceCollection services)
    {
        // 注册订阅Hangfire诊断日志
        DiagnosticListener.AllListeners.Subscribe(new BeeHangfireDiagnosticSourceSubscriber());
        // 注入Hangfire基础服务
        services.AddBeeHangfire();
        // TraceId替换为Elastic.Apm提供
        services.AddTransient<ApmProviderDistributedTraceId>();
        var provider = services.BuildServiceProvider().GetRequiredService<ApmProviderDistributedTraceId>();
        // 注册Hangfire全局过滤器
        GlobalConfiguration.Configuration.UseFilter(new BeeHangfireDiagnosticClientFilter(provider));
        GlobalConfiguration.Configuration.UseFilter(new BeeHangfireDiagnosticServerFilter());
        return services;
    }
}