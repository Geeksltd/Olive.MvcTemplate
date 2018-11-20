using MSharp;

namespace Modules
{
    public class LoginForm : FormModule<Domain.User>
    {
        public LoginForm()
        {
            SupportsAdd(false).Using("Olive.Security")
                .SupportsEdit(false)
                .Header("@(await Component.InvokeAsync<ContentBlockView>(new ViewModel.ContentBlockView {Key=\"LoginIntro\"}))")
                .HeaderText("Login")
                .DataSource("await Domain.User.FindByEmail(info.Email)");

            Field(x => x.Email).WatermarkText("Your email");
            Field(x => x.Password).Mandatory().WatermarkText("Your password");

            Button("Login").ValidateAntiForgeryToken(false).CssClass("w-100 btn-login mb-2")
            .OnClick(x =>
            {
                x.RunInTransaction(false);
                x.ShowPleaseWait();
                x.CSharp("var authenticationResult = await Olive.Security.Auth0.Authenticate(info.Email, info.Password);");
                x.If("!authenticationResult.Success")
                   .CSharp(@"Notify(authenticationResult.Message, ""error"");
                             return View(info); ");
                x.CSharp("await info.Item.LogOn();");
                x.If(CommonCriterion.RequestHas_ReturnUrl).ReturnToPreviousPage();
                x.Go<Login.DispatchPage>();
            });

            Link("Forgot password?").CssClass("text-info").OnClick(x => x.Go<Login.ForgotPasswordPage>());

            Reference<ContentBlockView>();
        }
    }
}
