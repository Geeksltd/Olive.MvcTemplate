using MSharp;
using Domain;

public class LoginPage : RootPage
{
    public LoginPage()
    {
        Route(@"login
            [#EMPTY#]");

        Add<Modules.LoginForm>();
        Add<Modules.SocialMediaLogin>();

        OnStart(x =>
        {
            x.If("Request.IsAjaxPost()").CSharp("return Redirect(Url.CurrentUri().OriginalString);");
            x.If("User.Identity.IsAuthenticated").Go<Login.DispatchPage>().RunServerSide();
            x.If("Url.ReturnUrl().IsEmpty()").Go("/login").RunServerSide()
                .Send("ReturnUrl", ValueContext.Static, "/login");
        });
    }
}
