using MSharp;
using Domain;

namespace Modules
{
    public class LoginForm : FormModule<Domain.User>
    {
        public LoginForm()
        {
            SupportsAdd(false)
                .SupportsEdit(false)
                .Header("@(await Component.InvokeAsync<ContentBlockView>(new ViewModel.ContentBlockView {Key=\"LoginIntro\"}))")
                .HeaderText("Please Login")
                .DataSource("await User.FindByEmail(info.Email)");

            Field(x => x.Email).WatermarkText("Your email");
            Field(x => x.Password).Mandatory().WatermarkText("Your password");
            CustomField().Label("Enter the code shown").PropertyName("CaptchaImage")
                .ControlMarkup("@*Html.Captcha(6, \"Captcha\", info.CaptchaSettings)*@")
                .VisibleIf("info.MustShowCaptcha");

            //================ Code Extensions: ================

            ViewModelProperty<bool>("MustShowCaptcha");

            ViewModelProperty("Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary", "CaptchaSettings").Getter(@"new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
            {
                {""RefreshButtonText"", ""Refresh the captcha image""},
                {""RequiredMessage"", ""Enter the code shown in the image""}
                }");

            ViewModelProperty<bool>("IsCaptchaValid");

            ViewModelProperty<bool>("SuccessfulLogin").Getter(@"if (MustShowCaptcha && !IsCaptchaValid) return false;
                
                return !InvalidCredentials && !Item.IsDeactivated;");

            ViewModelProperty<bool>("InvalidCredentials").Getter("Item == null || !SecurePassword.Verify(Password, Item.Password, Item.Salt)");

            OnBound("Should show captcha?").Criteria("Request.IsPost()")
                .Code("info.MustShowCaptcha = await LogonFailure.MustShowCaptcha(info.Email, Request.GetIPAddress());");

            OnBound("Captcha result").Code(@"// TODO: Find a .NET Core option for captcha:
                // info.IsCaptchaValid = this.IsCaptchaValid(""Capture code was invalid"");");

            Link("Forgot password?").Icon(FA.Key)
                .OnClick(x => x.Go<Login.ForgotPasswordPage>());

            Button("Login").ValidateAntiForgeryToken(false)
            .OnClick(x =>
            {
                x.ShowPleaseWait();
                x.If("info.InvalidCredentials").MessageBox("Invalid username and/or password. Please try again.").Style("error");
                x.If("info.Item.IsDeactivated").MessageBox("Your account is currently deactivated. It might be due to security concerns on your account. Please contact the system administrator to resolve this issue. We apologise for the inconvenience.").Style("error");
                x.If("!info.SuccessfulLogin").CSharp("info.CaptchaImage_Visible = await LogonFailure.NextAttemptNeedsCaptcha(info.Email, Request.GetIPAddress());");
                x.If("!info.SuccessfulLogin").ReturnView();
                x.CSharp("await LogonFailure.Remove(info.Email, Request.GetIPAddress());");
                x.CSharp("info.Item.LogOn();");
                x.If("Url.ReturnUrl().HasValue()").ReturnToPreviousPage();
                x.Go<Login.DispatchPage>();
            });

            Reference<ContentBlockView>();
        }
    }
}