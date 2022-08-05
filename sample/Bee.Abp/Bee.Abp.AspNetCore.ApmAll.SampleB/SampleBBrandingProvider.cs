using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Bee.Abp.AspNetCore.ApmAll.SampleB;

[Dependency(ReplaceServices = true)]
public class SampleBBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "SampleB";
}
