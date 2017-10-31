using MSharp;
using Domain;

namespace Login
{
    public class ResetPasswordPage : SubPage<Root.LoginPage>
    {
        public ResetPasswordPage()
        {
            Route("password/reset/{ticket}");
            
            Add<Modules.ResetUserPassword>();
        }
    }
}