using Bee.Abp.Localization;

namespace Bee.Abp;

[DependsOn(typeof(AbpAutofacModule))]
[DependsOn(typeof(AbpJsonModule))]
[DependsOn(typeof(AbpValidationModule))]
public class BeeAbpLocalizationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<BeeAbpLocalizationModule>(BeeAbpLocalizationConsts.NameSpace);
        });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Add<BeeAbpLocalizationResource>(BeeAbpLocalizationConsts.DefaultCultureName)
                .AddBaseTypes(typeof(AbpValidationResource)
                ).AddVirtualJson(BeeAbpLocalizationConsts.DefaultLocalizationResourceVirtualPath);

            options.DefaultResourceType = typeof(BeeAbpLocalizationResource);
        });

        Configure<AbpExceptionLocalizationOptions>(options =>
        {
            options.MapCodeNamespace(BeeAbpLocalizationConsts.NameSpace, typeof(BeeAbpLocalizationResource));
        });
    }
}