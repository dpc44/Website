using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Website.Data;
using Volo.Abp.DependencyInjection;

namespace Website.EntityFrameworkCore;

public class EntityFrameworkCoreWebsiteDbSchemaMigrator
    : IWebsiteDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreWebsiteDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the WebsiteDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<WebsiteDbContext>()
            .Database
            .MigrateAsync();
    }
}
