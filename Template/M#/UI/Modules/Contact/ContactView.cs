using MSharp;

namespace Modules
{
    public class ContactView : ViewModule<Domain.Contact>
    {
        public ContactView()
        {
            HeaderText("@item.Name Details");

            Field(x => x.Name);

            Field(x => x.PhoneNumber);

            Button("Back")
                .IsDefault()
                .Icon(FA.ChevronLeft)
                .OnClick(x => x.ReturnToPreviousPage());

            Button("Delete")
                .ConfirmQuestion("Are you sure you want to delete this Contact?")
                .CssClass("btn-danger")
                .Icon(FA.Remove)
                .OnClick(x =>
                {
                    x.DeleteItem();
                    x.GentleMessage("Deleted successfully.");
                    x.ReturnToPreviousPage();
                });
        }
    }
}
