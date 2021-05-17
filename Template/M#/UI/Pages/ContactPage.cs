using MSharp;

public class ContactPage : RootPage
{
    public ContactPage()
    {
        Add<Modules.MainMenu>();

        OnStart(x => x.Go<Contact.ContactsPage>().RunServerSide());
    }
}
