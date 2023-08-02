using Website.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Website.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class WebsiteController : AbpControllerBase
{
    protected WebsiteController()
    {
        LocalizationResource = typeof(WebsiteResource);
    }
}
