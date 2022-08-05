using System.Text;
using Elastic.Apm.SerilogEnricher;
using Elastic.CommonSchema.Serilog;
using Serilog;
using Serilog.Sinks.Elasticsearch;

namespace Bee.Abp.AspNetCore.ApmAll.SampleA;

public static class SerilogToEsExtensions
{
    public static void SetSerilogConfiguration(LoggerConfiguration loggerConfiguration, IConfiguration configuration, IHostEnvironment env)
    {
        loggerConfiguration
            .ReadFrom.Configuration(configuration)
            .Enrich.FromLogContext()
            .WriteTo.Console();

        var esUri = env.IsDevelopment() ? "http://localhost:9200" : "http://log-es-master.default.svc.cluster.local:9200";
        
        loggerConfiguration
            .Enrich.FromLogContext()
            .Enrich.WithElasticApmCorrelationInfo()
            //.Enrich.WithExceptionDetails()
            .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(esUri))
            {
                CustomFormatter = new EcsTextFormatter(),
                ModifyConnectionSettings = x => x.BasicAuthentication("admin", "changeme")
            });

        //loggerConfiguration.Enrich.WithProperty("Application", applicationName);
    }

}