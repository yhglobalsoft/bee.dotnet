namespace Bee.Abp.Domains;

public class BeeDomainServiceTests : BeeAbpTestBase
{
    private readonly TestDomainService _service;

    public BeeDomainServiceTests()
    {
        _service = GetRequiredService<TestDomainService>();
    }

    [Fact]
    public void Test()
    {
            
        using (CultureHelper.Use("en"))
        {
            var exception = Should.Throw<Exception>(() =>
            {
                _service.GetList();
            });
            exception.Message.ShouldBe("'{valueName}'值无效.");
        }

        using (CultureHelper.Use("zh-Hans"))
        {
            var exception = Should.Throw<Exception>(() =>
            {
                _service.GetList();
            });
            exception.Message.ShouldBe("'{valueName}'值无效.");
        }
    }
}