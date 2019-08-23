using Domain;
using MSharp;

namespace Modules
{
    public class Footer : GenericModule
    {
        const string DEVELOPER = "http://www.geeks.ltd.uk";
        const string LINKED_IN = "http://www.linkedin.com/company/geeks-ltd";
        const string EMAIL = "http://www.geeks.ltd.uk/software-development-quote.html";
        const string TWITTER = "https://twitter.com/GeeksLtd";
        const string FACEBOOK = "https://www.facebook.com/geeksltd";

        public Footer()
        {
            IsInUse().IsViewComponent()
                .Using("Olive.Security")
                .RootCssClass("website-footer")
                .Markup(@"
           <div>
               [#BUTTONS(Email)#]
               [#BUTTONS(LinkedIn)#]
               [#BUTTONS(Facebook)#]
               [#BUTTONS(Twitter)#] <br/>
               [#BUTTONS(SoftwareDevelopment)#] by [#BUTTONS(Geeks)#]
                &copy; @LocalTime.Now.Year. All rights reserved.
            </div>");



            Link("Geeks")
                .OnClick(x => x.Go(DEVELOPER, OpenIn.NewBrowserWindow));

            Link("Email")
                .NoText()
                .Icon(FA.EnvelopeSquare, 3)
                .Tooltip("Contact Us")
                .OnClick(x => x.Go(EMAIL, OpenIn.NewBrowserWindow));

            Link("LinkedIn")
                .NoText()
                .Icon(FA.LinkedinSquare, 3)
                .Tooltip("LinkedIn")
                .OnClick(x => x.Go(LINKED_IN, OpenIn.NewBrowserWindow));

            Link("Facebook")
                .NoText()
                .Icon(FA.FacebookSquare, 3)
                .Tooltip("Facebook")
                .OnClick(x => x.Go(FACEBOOK, OpenIn.NewBrowserWindow));

            Link("Twitter")
                .NoText()
                .Icon(FA.TwitterSquare, 3)
                .Tooltip("Twitter")
                .OnClick(x => x.Go(TWITTER, OpenIn.NewBrowserWindow));

            Link("Software development")
                .CssClass("plain-text")
                .OnClick(x => x.Go(DEVELOPER, OpenIn.NewBrowserWindow));
        }
    }
}