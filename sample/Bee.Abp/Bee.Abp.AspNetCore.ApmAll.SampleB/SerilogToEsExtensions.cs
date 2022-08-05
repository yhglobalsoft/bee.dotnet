using System.Text;
using Elastic.Apm.SerilogEnricher;
using Elastic.CommonSchema.Serilog;
using Serilog;
using Serilog.Sinks.Elasticsearch;

namespace Bee.Abp.AspNetCore.ApmAll.SampleB;

public static class SerilogToEsExtensions
{
    public static void SetSerilogConfiguration(LoggerConfiguration loggerConfiguration, IConfiguration configuration)
    {
        loggerConfiguration
            .ReadFrom.Configuration(configuration)
            .Enrich.FromLogContext()
            .WriteTo.Console();

        loggerConfiguration
            .Enrich.FromLogContext()
            .Enrich.WithElasticApmCorrelationInfo()
            //.Enrich.WithExceptionDetails()
            .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://localhost:9200"))
            {
                CustomFormatter = new EcsTextFormatter()
            });

        //loggerConfiguration.Enrich.WithProperty("Application", applicationName);
    }

}