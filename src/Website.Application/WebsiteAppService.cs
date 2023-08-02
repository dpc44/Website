using System;
using System.Collections.Generic;
using System.Text;
using Website.Localization;
using Volo.Abp.Application.Services;

namespace Website;

/* Inherit your application services from this class.
 */
public abstract class WebsiteAppService : ApplicationService
{
    protected WebsiteAppService()
    {
        LocalizationResource = typeof(WebsiteResource);
    }
}
