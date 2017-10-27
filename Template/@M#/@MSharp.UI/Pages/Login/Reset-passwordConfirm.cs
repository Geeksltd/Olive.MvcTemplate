using MSharp;
using Domain;

namespace Login.ResetPassword
{
    public class ConfirmPage : SubPage<ResetPasswordPage>
    {
        public ConfirmPage()
        {
            Add<Modules.ConfirmPasswordReset>();
        }
    }
}