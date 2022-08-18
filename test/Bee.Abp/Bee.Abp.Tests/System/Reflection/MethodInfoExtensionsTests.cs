using Bee.UnitTests;

namespace System.Reflection;

public class MethodInfoExtensionsTests
{
    [Fact]
    public void IsAsyncTest()
    {
        var type = typeof(TestEntity);
        var methods = type.GetMethods();
        var methodAsync = methods.First(e => e.Name == "TestMethodAsync");
        methodAsync.IsAsync().ShouldBeTrue();

        var methodIntAsync = methods.First(e => e.Name == "TestMethodIntAsync");
        methodIntAsync.IsAsync().ShouldBeTrue();

        var method = methods.First(e => e.Name == "TestMethod");
        method.IsAsync().ShouldBeFalse();
    }
}