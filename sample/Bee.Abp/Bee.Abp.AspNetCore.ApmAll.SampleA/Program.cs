using System;
using Bee.Abp.AspNetCore.ApmAll.SampleA.Data;
using Serilog;
using Serilog.Events;


namespace Bee.Abp.AspNetCore.ApmAll.SampleA;

public class Program
{
    public async static Task<int> Main(string[] args)
    {
        var loggerConfiguration = new LoggerConfiguration()
#if DEBUG
            .MinimumLevel.Debug()
#else
            .MinimumLevel.Information()
#endif
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .WriteTo.Async(c => c.File("Logs/logs.txt"))
            .WriteTo.Async(c => c.Console());

        if (IsMigrateDatabase(args))
        {
            loggerConfiguration.MinimumLevel.Override("Volo.Abp", LogEventLevel.Warning);
            loggerConfiguration.MinimumLevel.Override("Microsoft", LogEventLevel.Warning);
            loggerConfiguration.MinimumLevel.Override("IdentityServer4.Startup", LogEventLevel.Warning);
        }

        Log.Logger = loggerConfiguration.CreateLogger();

        try
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Host.AddAppSettingsSecretsJson()
                .UseAutofac()
                .UseSerilog((context, loggerConfiguration) =>
                {
                    SerilogToEsExtensions.SetSerilogConfiguration(loggerConfiguration, context.Configuration,
                        context.HostingEnvironment);
                });
            await builder.AddApplicationAsync<SampleAModule>();
            var app = builder.Build();
            await app.InitializeApplicationAsync();

            if (IsMigrateDatabase(args))
            {
                await app.Services.GetRequiredService<SampleADbMigrationService>().MigrateAsync();
                return 0;
            }

            Log.Information("Starting Bee.Abp.AspNetCore.ApmAll.SampleA.");
            await app.RunAsync();
            return 0;
        }
        catch (Exception ex)
        {
            if (ex.GetType().Name.Equals("StopTheHostException", StringComparison.Ordinal))
            {
                throw;
            }

            Log.Fatal(ex, "Bee.Abp.AspNetCore.ApmAll.SampleA terminated unexpectedly!");
            return 1;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    private static bool IsMigrateDatabase(string[] args)
    {
        return args.Any(x => x.Contains("--migrate-database", StringComparison.OrdinalIgnoreCase));
    }
}
