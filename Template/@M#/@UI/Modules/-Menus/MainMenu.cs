using MSharp;
using Domain;

namespace Modules
{
    public class MainMenu : MenuModule
    {
        public MainMenu()
        {
            AjaxRedirect().IsViewComponent().UlCssClass("nav navbar-nav dropped-submenu");

            Item("Login").Icon(FA.UnlockAlt).Visibility("false")
                .OnClick(x => x.Go<Root.LoginPage>());

            Item("Settings").Icon(FA.Cog)
                .OnClick(x => x.Go<Admin.SettingsPage>());
        }
    }
}