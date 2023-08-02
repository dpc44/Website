using Website.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Website.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(WebsiteEntityFrameworkCoreModule),
    typeof(WebsiteApplicationContractsModule)
    )]
public class WebsiteDbMigratorModule : AbpModule
{
}
