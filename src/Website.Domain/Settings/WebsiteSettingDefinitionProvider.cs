using Volo.Abp.Settings;

namespace Website.Settings;

public class WebsiteSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(WebsiteSettings.MySetting1));
    }
}
