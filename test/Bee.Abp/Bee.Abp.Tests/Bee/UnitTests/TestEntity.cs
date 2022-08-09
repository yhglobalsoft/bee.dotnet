using System.ComponentModel;

namespace Bee.UnitTests;

[Description("测试实体")]
[Serializable]
public class TestEntity
{
    [Description("编号")] public int Id { get; set; }

    [Description("名称")] public string Name { get; set; }

    [Description("添加时间")] public DateTime AddDate { get; set; }

    [Description("是否删除")] public bool IsDeleted { get; set; }

    public virtual List<TestEntity> TestEntities { get; set; } = new List<TestEntity>();

    public TestEntity()
    {
        AddDate = DateTime.Now;
        IsDeleted = false;
    }

    public async Task TestMethodAsync()
    {
        await Task.FromResult("");
    }

    public async Task<int> TestMethodIntAsync()
    {
        return await Task.FromResult(1);
    }

    public void TestMethod()
    {
    }
}