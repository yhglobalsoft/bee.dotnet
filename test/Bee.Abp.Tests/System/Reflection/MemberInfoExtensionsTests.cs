using Bee.UnitTests;
using Shouldly;
using Xunit;

namespace System.Reflection
{
    public class MemberInfoExtensionsTests
    {

        [Fact]
        public void ToDescriptionTest()
        {
            var type = typeof(TestEntity);
            var property = type.GetProperty("Id");
            property!.GetDescription().ShouldBe("编号");
        }
    }
}