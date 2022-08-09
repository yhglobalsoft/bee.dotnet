using Bee.Abp.Localization;

namespace Bee.Abp.Domains;

public class TestDomainService : BeeDomainService
{
    public TestDomainService()
    {
        LocalizationResource = typeof(BeeAbpLocalizationResource);
    }

    public void GetList()
    {
        throw new Exception(L["Bee.Abp:0101"].Value);
    }
}