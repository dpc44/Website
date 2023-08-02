using Website.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Website.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class WebsitePageModel : AbpPageModel
{
    protected WebsitePageModel()
    {
        LocalizationResourceType = typeof(WebsiteResource);
    }
}
