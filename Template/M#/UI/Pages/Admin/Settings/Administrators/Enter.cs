using MSharp;
using Domain;

namespace Admin.Settings.Administrators
{
    public class EnterPage : SubPage<AdministratorsPage>
    {
        public EnterPage()
        {
            Layout(Layouts.AdminDefaultModal);

            Add<Modules.AdministratorForm>();
        }
    }
}