using System.Diagnostics;
using Elastic.Apm;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace Bee.Hangfire.Diagnostics.Sample.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static ActivitySource source = new ActivitySource(BeeHangfireDiagnosticConsts.DiagnosticListenerName, "1.0.0");
    private readonly IConnectionMultiplexer _connectionMultiplexer;

    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IConnectionMultiplexer connectionMultiplexer)
    {
        _logger = logger;
        _connectionMultiplexer = connectionMultiplexer;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        var transaction = Agent.Tracer.CurrentTransaction;;
        RecurringJob.AddOrUpdate("测试RecurringJob122", () => TestJob(), CronType.Minute(1), TimeZoneInfo.Local);
        //BackgroundJob.Enqueue(() => TestJob());

        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task TestJob()
    {
        var httpclient = new HttpClient();
        var response = await httpclient.GetAsync("https://www.baidu.com");
        response.EnsureSuccessStatusCode();
        await GetCacheAsync();

        Console.WriteLine("hello");
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task GetCacheAsync()
    {
        var database = _connectionMultiplexer.GetDatabase();
        await database.StringSetAsync($"string1", 1);
        await database.StringGetAsync($"string1");
    }
}