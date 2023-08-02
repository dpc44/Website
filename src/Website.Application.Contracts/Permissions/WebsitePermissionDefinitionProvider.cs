using Website.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Website.Permissions;

public class WebsitePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(WebsitePermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(WebsitePermissions.MyPermission1, L("Permission:MyPermission1"));

        var myGroup2 = context.AddGroup("WebsiteArticlePermission", L("WebsiteArticlePermission"));
        var parent = myGroup2.AddPermission("ArticleSection");
        parent.AddChild("Website.ArticleCreate", L("WebsiteArticleCreate"));
        parent.AddChild("Website.ArticleDelete", L("WebsiteArticleDelete"));
        parent.AddChild("Website.ArticleList", L("WebsiteArticleList"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<WebsiteResource>(name);
    }
}
