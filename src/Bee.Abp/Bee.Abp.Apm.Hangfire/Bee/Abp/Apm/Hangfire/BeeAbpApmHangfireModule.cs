namespace Bee.Abp.Apm.Hangfire;

[DependsOn(typeof(BeeAbpModule))]
public class BeeAbpApmHangfireModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddBeeApmHangfire();
    }
}