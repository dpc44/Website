using Website.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Website;

[DependsOn(
    typeof(WebsiteEntityFrameworkCoreTestModule)
    )]
public class WebsiteDomainTestModule : AbpModule
{

}
