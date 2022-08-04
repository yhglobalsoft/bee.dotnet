using Elastic.Apm.DiagnosticSource;
using Elastic.Apm.Elasticsearch;
using Elastic.Apm.EntityFrameworkCore;
using Elastic.Apm.SqlClient;
using Elastic.Apm.StackExchange.Redis;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;


namespace Microsoft.AspNetCore;

public static class ApplicationBuilderExtensions
{
    public static void UseBeeApm(this IApplicationBuilder app, IConfiguration configuration = null)
    {
        Elastic.Apm.AspNetCore.ApmMiddlewareExtension.UseElasticApm(app, configuration,
            new HttpDiagnosticsSubscriber(),
            new EfCoreDiagnosticsSubscriber(),
            new SqlClientDiagnosticSubscriber(),
            new ElasticsearchDiagnosticsSubscriber()
        );
        UseBeeRedisApm(configuration, app);
    }

    /// <summary>
    /// 开启Redis APM 监控
    /// </summary>
    private static void UseBeeRedisApm(IConfiguration configuration, IApplicationBuilder app)
    {
        var muxer = app.ApplicationServices.GetService<IConnectionMultiplexer>();
        muxer.UseElasticApm();
    }
}