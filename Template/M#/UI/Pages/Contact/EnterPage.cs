using MSharp;

namespace Contact
{
    public class EnterPage : SubPage<ContactsPage>
    {
        public EnterPage()
        {
            Layout(Layouts.AdminDefaultModal);

            Add<Modules.ContactForm>();
        }
    }

}
