using MSharp;
using Domain;

namespace Admin.Settings
{
    public class AdministratorsPage : SubPage<SettingsPage>
    {
        public AdministratorsPage()
        {
            Add<Modules.AdministratorsList>();
        }
    }
}