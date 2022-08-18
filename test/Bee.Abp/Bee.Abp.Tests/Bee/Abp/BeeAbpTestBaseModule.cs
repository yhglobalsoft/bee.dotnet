namespace Bee.Abp;

[DependsOn(typeof(BeeAbpModule))]
[DependsOn(typeof(BeeAbpLocalizationModule))]
[DependsOn(typeof(AbpAutofacModule))]
[DependsOn(typeof(AbpTestBaseModule))]
public class BeeAbpTestBaseModule : AbpModule
{
}