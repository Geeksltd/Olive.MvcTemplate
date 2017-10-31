using MSharp;
using Domain;

namespace Modules
{
    public class ViewAdmin : ViewModule<Domain.Administrator>
    {
        public ViewAdmin()
        {
            HideEmptyElements().HeaderText("@item Details");

            Field(x => x.Name);

            Field(x => x.Email);

            Field(x => x.IsDeactivated);

            //================ Buttons: ================

            Button("Back").Icon(FA.ChevronLeft)
                .Action(x => x.ReturnToPreviousPage());

            Button("Delete").Icon(FA.Remove)
               .Action(x => { x.DeleteItem(); x.ReturnToPreviousPage(); });
        }
    }
}