using Shouldly;

namespace Bee.Abp.Dto;

public class QueryPagingInputTest
{
    [Fact]
    public void Test()
    {
        var input1 = new BeeQueryPagingInput();
        input1.PageIndex.ShouldBe(1);
        input1.PageSize.ShouldBe(10);

        var input2 = new BeeQueryPagingInput(2, 20);
        input2.PageIndex.ShouldBe(2);
        input2.PageSize.ShouldBe(20);

        input2.SkipCount.ShouldBe(20);
    }
}