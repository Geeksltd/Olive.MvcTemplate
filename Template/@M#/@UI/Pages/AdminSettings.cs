using MSharp;
using Domain;

namespace Admin
{
    public class SettingsPage : SubPage<Root.AdminPage>
    {
        public SettingsPage()
        {
            Set(PageSettings.LeftMenu, "AdminSettingsMenu");
            
            StartUp(x => x.Go<Settings.GeneralPage>().RunServerSide());
        }
    }
}