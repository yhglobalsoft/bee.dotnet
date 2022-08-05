using Microsoft.EntityFrameworkCore;
using Volo.Abp.DependencyInjection;

namespace Bee.Abp.AspNetCore.ApmAll.SampleA.Data;

public class SampleAEFCoreDbSchemaMigrator : ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public SampleAEFCoreDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the SampleADbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<SampleADbContext>()
            .Database
            .MigrateAsync();
    }
}
