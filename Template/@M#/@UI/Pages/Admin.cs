using MSharp;
using Domain;


public class AdminPage : RootPage
{
    public AdminPage()
    {
        Roles(AppRole.Administrator);

        Add<Modules.MainMenu>();

        OnStart(x => x.Go<Admin.SettingsPage>().RunServerSide());
    }
}