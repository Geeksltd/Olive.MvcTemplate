using MSharp;
using Domain;

namespace Login
{
    public class ForgotPasswordPage : SubPage<LoginPage>
    {
        public ForgotPasswordPage()
        {
            Add<Modules.RequestUserPasswordResetTicket>();
        }
    }
}