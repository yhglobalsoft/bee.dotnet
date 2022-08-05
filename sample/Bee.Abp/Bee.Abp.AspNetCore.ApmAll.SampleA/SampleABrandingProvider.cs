using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Bee.Abp.AspNetCore.ApmAll.SampleA;

[Dependency(ReplaceServices = true)]
public class SampleABrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "SampleA";
}
