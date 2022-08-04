using Bee.Abp;
using Volo.Abp;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Bee.Abp
{

    [DependsOn(typeof(BeeAbpLocalizationModule))]
    [DependsOn(typeof(AbpAutofacModule))]
    [DependsOn(typeof(AbpTestBaseModule))]
    public class BeeAbpLocalizationTestBaseModule : AbpModule
    {
    }
}
