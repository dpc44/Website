using System.Threading.Tasks;

namespace Website.Data;

public interface IWebsiteDbSchemaMigrator
{
    Task MigrateAsync();
}
