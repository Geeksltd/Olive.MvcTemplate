using MSharp;
using Domain;

namespace Modules
{
    public class AdminSettingsMenu : MenuModule
    {
        public AdminSettingsMenu()
        {
            IsViewComponent()
                .SpecialSelectedKeyRule(@"// Example:
            
            if (Request.Has("".category""))
                return Request.GetValue("".category"");")
                .UlCssClass("nav nav-stacked dropped-submenu").RootCssClass("navbar navbar-default");
            
            Item("General settings")
                .Action(x => x.Go<Admin.Settings.GeneralPage>());
            
            Item("Administrators")
                .Action(x => x.Go<Admin.Settings.AdministratorsPage>());
            
            Item("Email templates")
                .Action(x => x.Go<Admin.Settings.EmailTemplatesPage>());
            
            Item("Content blocks")
                .Action(x => x.Go<Admin.Settings.ContentBlocksPage>());
        }
    }
}