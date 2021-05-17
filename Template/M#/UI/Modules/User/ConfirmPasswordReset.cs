using MSharp;
using Domain;

namespace Modules
{
    public class ConfirmPasswordReset : ViewModule<Domain.User>
    {
        public ConfirmPasswordReset()
        {
            HeaderText("@item Details");

            Link("Proceed to the login page.").OnClick(x => x.Go<LoginPage>());
        }
    }
}