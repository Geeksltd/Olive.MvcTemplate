using MSharp;

namespace Modules
{
    public class ContactsList : ListModule<Domain.Contact>
    {
        public ContactsList()
        {
            HeaderText("Contacts")
                .UseDatabasePaging(false)
                .Sortable()
                .PageSize(10)
                .ShowFooterRow()
                .ShowHeaderRow()
                .PagerPosition(PagerAt.Bottom);

            Search(GeneralSearch.AllFields).Label("Find");

            SearchButton("Search").OnClick(x => x.Reload());

            ButtonColumn("c#:item.Name")
                .Style(ButtonStyle.Link)
                .HeaderText("Name")
                .OnClick(x => x.Go<Contact.ViewPage>()
                .Send("item", "item.ID"));

            Column(x => x.PhoneNumber);

            ButtonColumn("Edit")
                .HeaderText("Actions")
                .GridColumnCssClass("actions")
                .Icon(FA.Edit)
                .OnClick(x => x.PopUp<Contact.EnterPage>()
                .Send("item", "item.ID").SendReturnUrl(false));

            ButtonColumn("Delete")
                .HeaderText("Actions")
                .GridColumnCssClass("actions")
                .ConfirmQuestion("Are you sure you want to delete this Contact?")
                .CssClass("btn-danger")
                .Icon(FA.Remove)
                .OnClick(x =>
                {
                    x.DeleteItem();
                    x.RefreshPage();
                });

            Button("New Contact")
                .Icon(FA.Plus)
                .OnClick(x => x.PopUp<Contact.EnterPage>().SendReturnUrl(false));
        }
    }

}
