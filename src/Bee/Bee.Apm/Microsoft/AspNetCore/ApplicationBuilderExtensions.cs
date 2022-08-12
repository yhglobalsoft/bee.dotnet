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

public static class ApplicationBuilderBasicApmExtensions
{
    /// <summary>
    /// 开启 基础 APM 监控
    /// 目前已支持 Http EfCore SqlClient Elasticsearch
    /// hangfire Cap 等实现中
    /// </summary>
    public static IApplicationBuilder UseBeeBasicApm(this IApplicationBuilder app, IConfiguration configuration = null)
    {
        Elastic.Apm.AspNetCore.ApmMiddlewareExtension.UseElasticApm(
            app, 
            configuration,
            new HttpDiagnosticsSubscriber(),
            new EfCoreDiagnosticsSubscriber(),
            new SqlClientDiagnosticSubscriber(),
            new ElasticsearchDiagnosticsSubscriber()
        );
        return app;
    }
}


public static class ApplicationBuilderRedisApmExtensions
{
    /// <summary>
    /// 开启Redis APM 监控
    /// </summary>
    public static IApplicationBuilder UseBeeRedisApm(this IApplicationBuilder app)
    {
        var muxer = app.ApplicationServices.GetService<IConnectionMultiplexer>();
        muxer.UseElasticApm();
        return app;
    }
}