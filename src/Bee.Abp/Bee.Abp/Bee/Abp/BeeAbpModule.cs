using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.BlobStoring.Aliyun;
using Volo.Abp.Caching;
using Volo.Abp.Domain;
using Volo.Abp.Json;
using Volo.Abp.Modularity;
using Volo.Abp.ObjectMapping;
using Volo.Abp.TextTemplating.Scriban;
using Volo.Abp.VirtualFileSystem;

namespace Bee.Abp;

[DependsOn(typeof(AbpDddDomainModule))]
[DependsOn(typeof(AbpAutoMapperModule))]
[DependsOn(typeof(AbpObjectMappingModule))]
[DependsOn(typeof(AbpCachingModule))]
[DependsOn(typeof(AbpBlobStoringAliyunModule))]
[DependsOn(typeof(AbpDddApplicationContractsModule))]
[DependsOn(typeof(AbpTextTemplatingScribanModule))]
[DependsOn(typeof(BeeAbpLocalizationModule))]
public class BeeAbpModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<AbpJsonOptions>(options =>
        {
            options.UseHybridSerializer = false;
        });
    }
        
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<BeeAbpModule>(BeeAbpConsts.NameSpace);
        });
    }
}