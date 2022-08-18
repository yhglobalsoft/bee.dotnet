using System.ComponentModel;
using Bee.Filter;
using Bee.UnitTests;

namespace System.Linq;

public class QueryableExtensionsTests
{
    [Fact]
    public void WhereIfTest_IQueryable()
    {
        var source = new List<int> { 1, 2, 3, 4, 5, 6, 7 }.AsQueryable();

        // 条件不满足 不过滤
        source.WhereIf(m => m > 5, false).ToList().ShouldBe(source.ToList());

        // 条件满足 过滤
        var actual = new List<int> { 6, 7 };
        source.WhereIf(m => m > 5, true).ToList().ShouldBe(actual);
    }

    [Fact]
    public void OrderByTest_IQueryable()
    {
        var source = new List<TestEntity>
        {
            new() { Id = 1, Name = "abc" },
            new() { Id = 4, Name = "fda", IsDeleted = true },
            new() { Id = 6, Name = "rwg", IsDeleted = true },
            new() { Id = 3, Name = "hdg" },
        }.AsQueryable();

        source.OrderBy("Id").ToArray()[1].Name.ShouldBe("hdg");
        source.OrderBy("Name", ListSortDirection.Descending).ToArray()[3].Id.ShouldBe(1);
        source.OrderBy(new SortCondition("Id")).ToArray()[1].Name.ShouldBe("hdg");

        source.OrderBy(new SortCondition<TestEntity>(m => m.Id)).ToArray()[1].Name.ShouldBe("hdg");
        source.OrderBy(new SortCondition<TestEntity>(m => m.Name!.Length, ListSortDirection.Ascending))
            .ToArray()[1].Name.ShouldBe("fda");
        source.OrderBy(new SortCondition("Name", ListSortDirection.Descending)).ToArray()[3].Id.ShouldBe(1);
    }

    [Fact]
    public void ThenByTest_IQueryable()
    {
        var source = new List<TestEntity>
        {
            new() { Id = 1, Name = "abc" },
            new() { Id = 4, Name = "fda", IsDeleted = true },
            new() { Id = 6, Name = "rwg", IsDeleted = true },
            new() { Id = 3, Name = "hdg" },
        }.AsQueryable();

        source.OrderBy("IsDeleted").ThenBy("Id").ToArray()[2].Name.ShouldBe("fda");
        source.OrderBy("IsDeleted", ListSortDirection.Descending)
            .ThenBy("Id", ListSortDirection.Descending)
            .ToArray()[2].Name.ShouldBe("hdg");
        source.OrderBy(new SortCondition("IsDeleted")).ThenBy(new SortCondition("Name")).ToArray()[2].Name.ShouldBe("fda");
    }
}