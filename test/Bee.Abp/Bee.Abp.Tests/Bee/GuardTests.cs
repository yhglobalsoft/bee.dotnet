using Bee.Abp.Exceptions;
using Shouldly;

namespace Bee;

public class GuardTests
{
    [Fact]
    public void NotNull_Test()
    {
        Should.Throw<BeeNullException>(() => { Guard.NotNull<string>(null, "parameterName1"); });
        var value = "abc";
        var result = Guard.NotNull(value, "value");
        result.ShouldBe(value);
    }

    [Fact]
    public void NotNullOrEmpty_String_Test()
    {
        Should.Throw<BeeNullOrEmptyException>(() => { Guard.NotNullOrEmpty(null, "Name"); });
        Should.Throw<BeeNullOrEmptyException>(() => { Guard.NotNullOrEmpty("", "Name"); });

        var value1 = "abc";
        var result1 = Guard.NotNullOrEmpty(value1, nameof(value1));
        result1.ShouldBe(value1);

        var exception1 = Should.Throw<BeeLengthGreaterException>(() =>
        {
            Guard.NotNullOrEmpty(value1, nameof(value1), 2);
        });
        exception1.ShouldNotBeNull();

        var result2 = Guard.NotNullOrEmpty(value1, nameof(value1), 3);
        result2.ShouldBe(value1);

        var exception2 = Should.Throw<BeeLengthLessException>(() =>
        {
            Guard.NotNullOrEmpty(value1, nameof(value1), minLength: 4);
        });
        exception2.ShouldNotBeNull();

        var result3 = Guard.NotNullOrEmpty(value1, nameof(value1), 3, minLength: 1);
        result3.ShouldBe(value1);

        var value2 = "";
        value2 = Guard.Length(value2, nameof(value2), 2, 0);
        value2.ShouldBe("");
        value2 = null;
        value2 = Guard.Length(value2, nameof(value2), 2, 0);
        value2.ShouldBeNull();

        value2 = "abc";
        Should.Throw<BeeLengthGreaterException>(() => { Guard.Length(value2, nameof(value2), 2); });
        value2 = "abc";
        Should.Throw<BeeLengthLessException>(() => { Guard.Length(value2, nameof(value2), 4, 4); });
    }

    [Fact]
    public void NotEmpty_Guid_Test()
    {
        Should.Throw<BeeEmptyGuidException>(() => { Guard.NotEmpty(Guid.Empty, "Name"); });
        Guard.NotEmpty(Guid.NewGuid(), "Name");
    }

    [Fact]
    public void NotNullOrEmpty_Collection_Test()
    {
        Should.Throw<BeeCollectionNullOrEmptyException>(() =>
        {
            Guard.NotNullOrEmpty(new List<int>(), "Name");
        });
        Guard.NotNullOrEmpty(new List<int>() {1}, "Name");
    }

    [Fact]
    public void LessThan_Test()
    {
        Should.Throw<BeeOutOfRangeException>(() => { Guard.LessThan(0, "Name", 0, false); });
        Guard.LessThan(0, "Name", 0, true);
        Should.Throw<BeeOutOfRangeException>(() => { Guard.LessThan(1, "Name", 0, false); });
        Should.Throw<BeeOutOfRangeException>(() => { Guard.LessThan(1, "Name", 0, true); });
        Guard.LessThan(-1, "Name", 0, false);
        Guard.LessThan(-1, "Name", 0, true);
    }

    [Fact]
    public void GreaterThan_Test()
    {
        Should.Throw<BeeOutOfRangeException>(() => { Guard.GreaterThan(0, "Name", 0, false); });
        Guard.GreaterThan(0, "Name", 0, true);
        Should.Throw<BeeOutOfRangeException>(() => { Guard.GreaterThan(-1, "Name", 0, false); });
        Should.Throw<BeeOutOfRangeException>(() => { Guard.GreaterThan(-1, "Name", 0, true); });
        Guard.GreaterThan(1, "Name", 0, false);
        Guard.GreaterThan(1, "Name", 0, true);
    }

    [Fact]
    public void Between_Test()
    {
        Should.Throw<BeeOutOfRangeException>(() => { Guard.Between(0, "Name", 0, 0, false, false); });
        Should.Throw<BeeOutOfRangeException>(() => { Guard.Between(0, "Name", 0, 0, true, false); });
        Should.Throw<BeeOutOfRangeException>(() => { Guard.Between(0, "Name", 0, 0, false, true); });
        Guard.Between(0, "Name", 0, 0, true, true);
        Guard.Between(0, "Name", -1, 1, true, true);
    }

    [Fact]
    public void DirectoryExists_Test()
    {
        Should.Throw<BeeDirectoryNotFoundException>(() =>
        {
            Guard.DirectoryExists(Directory.GetCurrentDirectory() + "ABC", "Name");
        });
        Guard.DirectoryExists(Directory.GetCurrentDirectory(), "Name");
    }

    [Fact]
    public void FileExists_Test()
    {
        Should.Throw<BeeFileNotFoundException>(() => { Guard.FileExists("Bee.Abp.Tests1.dll", "Name"); });
        Guard.FileExists("Bee.Abp.Tests.dll", "Name");
    }

    [Fact]
    public void MethodNameTest()
    {
        Assert.True(typeof(int?).IsValueType);
        Assert.True(typeof(Nullable<>).IsValueType);
    }
}