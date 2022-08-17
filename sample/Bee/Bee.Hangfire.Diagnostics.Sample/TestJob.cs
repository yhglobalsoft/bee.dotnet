namespace Bee.Hangfire.Diagnostics.Sample;

public class TestJob
{
    public Task ExecuteAsync()
    {
        Console.WriteLine($"job 测试- {DateTime.Now}");
        return Task.CompletedTask;
    }
}