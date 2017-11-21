using MSharp;
using Domain;

namespace Admin
{
    public class SettingsPage : SubPage<Root.AdminPage>
    {
        public SettingsPage()
        {
            Set(PageSettings.LeftMenu, "AdminSettingsMenu");
            
            OnStart(x => x.Go<Settings.GeneralPage>().RunServerSide());
        }
    }
}