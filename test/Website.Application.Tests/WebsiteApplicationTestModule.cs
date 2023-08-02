using Volo.Abp.Modularity;

namespace Website;

[DependsOn(
    typeof(WebsiteApplicationModule),
    typeof(WebsiteDomainTestModule)
    )]
public class WebsiteApplicationTestModule : AbpModule
{

}
