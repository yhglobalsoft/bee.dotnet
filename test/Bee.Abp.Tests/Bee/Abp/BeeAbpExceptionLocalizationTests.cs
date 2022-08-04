using Bee.Abp.Exceptions.Domains;
using Shouldly;
using Volo.Abp.Localization;

namespace Bee.Abp
{
    public class BeeAbpExceptionLocalizationTests : BeeAbpTestBase
    {
        [Fact]
        public void Test()
        {
            using (CultureHelper.Use("en"))
            {
                var exception = Should.Throw<BeeOutOfRangeDomainException>(() => throw new BeeOutOfRangeDomainException(
                    "myParameterName",
                    false,
                    false
                ));
                exception.Code.ShouldBe(BeeAbpErrorCodes.OutOfRange);
            }
        }
    }
}