using MSharp;
using Domain;

namespace Root
{
    public class AdminPage : RootPage
    {
        public AdminPage()
        {
            Roles(Role.Administrator);
            
            Add<Modules.MainMenu>();
            
            StartUp(x => x.Go<Admin.SettingsPage>().RunServerSide());
        }
    }
}