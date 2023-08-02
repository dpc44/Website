using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace Website.Web;

[Dependency(ReplaceServices = true)]
public class WebsiteBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Website";
}
