using MSharp;

namespace Modules
{
    public class SocialMediaLogin : FormModule<Domain.User>
    {
        public SocialMediaLogin()
        {
            SupportsAdd(false).SupportsEdit(false).Header("<h3>Other ways to sign in</h3>")
                .Using("Olive.Security")
                .RootCssClass("input-form social-media-login p-4");

            CustomField().NoLabel()
                .ControlMarkup("<p>The email address you are registered with @Model.Provider is not registered with us. Please register with us first with the same email address and then you would be able to sign in through @Model.Provider </p>")
                .VisibleIf("@info?.Error == \"not-registered\"");

            CustomField().NoLabel()
                .ControlMarkup("<p>Although your login with @Model.Provider was successful but we cannot log you into our system because @Model.Provider did not supply us your email address. It might be due to security restrictions you have set with them.</p>")
                .VisibleIf("@info?.Error == \"deactivated\"");

            CustomField().NoLabel()
                .ControlMarkup("<p>Your account is currently deactivated. It might be due to security concerns on your account. Please contact the system administrator to resolve this issue. We apologise for the inconvenience.</p>")
                .VisibleIf("@info?.Error == \"deactivated\"");

            // ================ Code Extensions: ================

            ViewModelProperty<string>("Provider").FromRequestParam("provider");

            ViewModelProperty<string>("Error").FromRequestParam("error");

            Button("Facebook")
                .ExtraTagAttributes("formmethod='post'")
                .CssClass("btn-social-login btn-facebook")
                .Icon(FA.Facebook)
                .OnClick(x => x.CSharp("await OAuth.Instance.LoginBy(\"Facebook\");"));

            Button("Google")
                .ExtraTagAttributes("formmethod='post'")
                .CssClass("btn-social-login btn-google")
                .Icon(FA.GooglePlus)
                .OnClick(x => x.CSharp("await OAuth.Instance.LoginBy(\"Google\");"));

            // Button("Microsoft")
            //    .ExtraTagAttributes("formmethod='post'")
            //    .CssClass("btn-social btn-microsoft")
            //    .Icon(FA.Windows)
            //    .OnClick(x => x.CSharp("await OAuth.Instance.LoginBy(\"Microsoft\");"));
        }
    }
}