using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Website.Data;

/* This is used if database provider does't define
 * IWebsiteDbSchemaMigrator implementation.
 */
public class NullWebsiteDbSchemaMigrator : IWebsiteDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
