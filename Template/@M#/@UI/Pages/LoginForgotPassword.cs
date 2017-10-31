using MSharp;
using Domain;

namespace Login
{
    public class ForgotPasswordPage : SubPage<Root.LoginPage>
    {
        public ForgotPasswordPage()
        {
            Add<Modules.RequestUserPasswordResetTicket>();
        }
    }
}