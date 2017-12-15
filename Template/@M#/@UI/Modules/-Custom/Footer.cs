using MSharp;
using Domain;

namespace Modules
{
    public class Footer : GenericModule
    {
        public Footer()
        {
            IsInUse().IsViewComponent().Using("Olive.Security")
                .Markup(@"<div class=""pull-right"">
            [#BUTTONS(Email)#] [#BUTTONS(LinkedIn)#] [#BUTTONS(Facebook)#] [#BUTTONS(Twitter)#]
            <br/>
            [#BUTTONS(SoftwareDevelopment)#] by [#BUTTONS(Geeks)#]
            
            </div>
            
            <div>[#BUTTONS(Logout)#]</div>
            <div>&copy; @LocalTime.Now.Year. All rights reserved. </div>")
                .RootCssClass("website-footer");

            Link("Logout").ValidateAntiForgeryToken(false).Icon(FA.SignOut).MarkupTemplate("Hi @GetUser() ([#Button#])")
                .VisibleIf(AppRole.User)
                .OnClick(x => { x.CSharp("await OAuth.Instance.LogOff();"); x.Go<LoginPage>(); });

            Link("Geeks")
                .OnClick(x => x.Go("http://www.geeks.ltd.uk").WindowName("_blank"));

            Link("Email").NoText().Icon(FA.EnvelopeSquare, 3).Tooltip("Contact Us")
                .OnClick(x => x.Go("http://www.geeks.ltd.uk/software-development-quote.html").Target(OpenIn.NewBrowserWindow));

            Link("LinkedIn").NoText().Icon(FA.LinkedinSquare, 3).Tooltip("LinkedIn")
                .OnClick(x => x.Go("http://www.linkedin.com/company/geeks-ltd").Target(OpenIn.NewBrowserWindow));

            Link("Facebook").NoText().Icon(FA.FacebookSquare, 3).Tooltip("Facebook")
                .OnClick(x => x.Go("https://www.facebook.com/geeksltd").Target(OpenIn.NewBrowserWindow));

            Link("Twitter").NoText().Icon(FA.TwitterSquare, 3).Tooltip("Twitter")
                .OnClick(x => x.Go("https://twitter.com/GeeksLtd").Target(OpenIn.NewBrowserWindow));

            Link("Software development").CssClass("plain-text")
                .OnClick(x => x.Go("http://www.geeks.ltd.uk").WindowName("_blank"));
        }
    }
}