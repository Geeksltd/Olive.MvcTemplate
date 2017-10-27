using MSharp;
using Domain;

namespace Modules
{
    public class SocialMediaLogin : FormModule<Domain.User>
    {
        public SocialMediaLogin()
        {
            SupportsAdd(false).SupportsEdit(false).Header("<h3>Other ways to sign in</h3>")
                .RootCssClass("input-form social-media-login");
            
            CustomField().NoLabel()
                .ControlMarkup("<p>The email address you are registered with @Model.Provider is not registered with us. Please register with us first with the same email address and then you would be able to sign in through @Model.Provider </p>")
                .Visibility("@info.Error == \"not-registered\"");
            
            CustomField().NoLabel()
                .ControlMarkup("<p>Although your login with @Model.Provider was successful but we cannot log you into our system because @Model.Provider did not supply us your email address. It might be due to security restrictions you have set with them.</p>")
                .Visibility("@info.Error == \"deactivated\"");
            
            CustomField().NoLabel()
                .ControlMarkup("<p>Your account is currently deactivated. It might be due to security concerns on your account. Please contact the system administrator to resolve this issue. We apologise for the inconvenience.</p>")
                .Visibility("@info.Error == \"deactivated\"");
            
            //================ Code Extensions: ================
            
            Property<string>("Provider").FromRequestParam("provider");
            
            Property<string>("Error").FromRequestParam("error");
            
            //================ Buttons: ================
            
            Button("Login by google").CausesValidation(false).ExtraTagAttributes("formmethod=post")
                .CssClass("btn-social btn-google").Icon(FA.GooglePlus)
                .Action(x => x.CSharp("UserServices.LoginBy(\"Google\");"));
            
            Button("Login by facebook").CausesValidation(false).ExtraTagAttributes("formmethod=post")
                .CssClass("btn-social btn-facebook").Icon(FA.Facebook)
                .Action(x => x.CSharp("UserServices.LoginBy(\"Facebook\");"));
        }
    }
}