using System.Threading.Tasks;
using Website.Localization;
using Website.MultiTenancy;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.TenantManagement.Web.Navigation;
using Volo.Abp.UI.Navigation;

namespace Website.Web.Menus;

public class WebsiteMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var administration = context.Menu.GetAdministration();
        var l = context.GetLocalizer<WebsiteResource>();

        context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem(
                WebsiteMenus.Home,
                l["Menu:Home"],
                "~/",
                icon: "fas fa-home",
                order: 0
            )
        );

        if (MultiTenancyConsts.IsEnabled)
        {
            administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
        }
        else
        {
            administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
        }

        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
        administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 3);

        context.Menu.AddItem(
            new ApplicationMenuItem(
                "Website",
                l["Menu:Website"],
                icon: "fas fa-shopping-cart"
                    ).AddItem(
                new ApplicationMenuItem(
                    "ProductManagement.Articles",
                    l["Menu:Articles"],
                    url: "/Articles"))
                    .AddItem(
                new ApplicationMenuItem(
                    "ProductManagement.Articles",
                    l["Menu:CSV"],
                    url: "/CSV")));
        return Task.CompletedTask;
    }
}
