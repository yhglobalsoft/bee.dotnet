using Microsoft.Extensions.Localization;
using Shouldly;
using Bee.Abp.Localization;
using Volo.Abp.Localization;

namespace Bee.Abp
{
    public class LocalizationTests : BeeAbpLocalizationTestBase
    {

        private readonly IStringLocalizer<BeeAbpLocalizationResource> _stringLocalizer;

        public LocalizationTests()
        {
            _stringLocalizer = GetRequiredService<IStringLocalizer<BeeAbpLocalizationResource>>();
        }

        [Fact]
        public void Test()
        {
            using (CultureHelper.Use("en"))
            {
                _stringLocalizer["Bee.Abp:0101"].Value
                    .ShouldBe("'{valueName}'值无效.");
            }

            using (CultureHelper.Use("zh-Hans"))
            {
                _stringLocalizer["Bee.Abp:0101"].Value
                    .ShouldBe("'{valueName}'值无效.");
            }
        }

    }
}
