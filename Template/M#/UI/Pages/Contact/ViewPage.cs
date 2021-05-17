using MSharp;

namespace Contact
{
    public class ViewPage : SubPage<ContactsPage>
    {
        public ViewPage()
        {
            Layout(Layouts.AdminDefault);

            Set(PageSettings.LeftMenu, "AdminSettingsMenu");

            Add<Modules.ContactView>();
        }
    }

}
