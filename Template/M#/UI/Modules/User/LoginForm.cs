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
                x.CSharp("User user = null;");
                x.CSharp(@"user = await Domain.User.FindByEmail(info.Email);

                if (user == null)
                    throw new Olive.Entities.ValidationException(""Invalid username and/or password. Please try again."");

                if (user.IsDeactivated)
                    throw new Olive.Entities.ValidationException(""Your account is deactivated. Please contact an administrator."");

                if (!SecurePassword.Verify(info.Password, user.Password, user.Salt))
                    throw new Olive.Entities.ValidationException(""Invalid username and/or password. Please try again."");").ValidationError();
                x.If("user == null")
                   .CSharp(@" Notify(""Invalid login "", ""error""); return View(info); ");
                x.CSharp("info.Item = user; await info.Item.LogOn();");
                x.If(CommonCriterion.RequestHas_ReturnUrl).ReturnToPreviousPage();
                x.Go<Login.DispatchPage>();
            });

            Link("Forgot password?").CssClass("text-info").OnClick(x => x.Go<Login.ForgotPasswordPage>());

            Reference<ContentBlockView>();
        }
    }
}
