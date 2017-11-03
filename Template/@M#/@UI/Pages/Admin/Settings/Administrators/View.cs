using MSharp;
using Domain;

namespace Admin.Settings.Administrators
{
    public class ViewPage : SubPage<AdministratorsPage>
    {
        public ViewPage()
        {
            Add<Modules.ViewAdmin>();
        }
    }
}