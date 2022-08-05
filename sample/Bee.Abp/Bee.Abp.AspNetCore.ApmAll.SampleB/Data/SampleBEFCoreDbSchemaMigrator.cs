using Microsoft.EntityFrameworkCore;
using Volo.Abp.DependencyInjection;

namespace Bee.Abp.AspNetCore.ApmAll.SampleB.Data;

public class SampleBEFCoreDbSchemaMigrator : ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public SampleBEFCoreDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the SampleBDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<SampleBDbContext>()
            .Database
            .MigrateAsync();
    }
}
