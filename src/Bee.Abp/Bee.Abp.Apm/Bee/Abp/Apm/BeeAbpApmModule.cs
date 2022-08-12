using Microsoft.AspNetCore;
using Volo.Abp;
using Volo.Abp.AspNetCore;
using Volo.Abp.Caching.StackExchangeRedis;
using Volo.Abp.Modularity;

namespace Bee.Abp.Apm;


[DependsOn(typeof(BeeAbpModule))]
[DependsOn(typeof(AbpAspNetCoreModule))]
[DependsOn(typeof(AbpCachingStackExchangeRedisModule))]
public class BeeAbpApmModule : AbpModule
{
    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        app.UseBeeBasicApm(context.GetConfiguration());
    }
}