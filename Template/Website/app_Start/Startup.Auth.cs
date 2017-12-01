using System;
using System.Web;
using Domain;
using Olive;
using Olive.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;

public class StartupAuth
{
    public static void Configuration(AuthenticationBuilder authenticationBuilder)
    {
        EnableGoogle(authenticationBuilder);
        EnableFacebook(authenticationBuilder);
        EnableMicrosoft(authenticationBuilder);
    }

    private static void EnableMicrosoft(AuthenticationBuilder authenticationBuilder)
    {
        // Step 1: Go to https://apps.dev.microsoft.com, and create a new app.
        // Step 2: Add a web platform and set Redirect URLs as your home url plus 'signin-microsoft'.
        // Step 3: Generate a password and set the appsetting.json with it as well as application id.
        //          Authentication:Microsoft:ApplicationId & Authentication:Microsoft:Password

        authenticationBuilder.AddMicrosoftAccount(configureOptions =>
        {
            configureOptions.ClientId = Config.Get("Authentication:Microsoft:ApplicationId");
            configureOptions.ClientSecret = Config.Get("Authentication:Microsoft:Password");
        });
    }

    private static void EnableFacebook(AuthenticationBuilder authenticationBuilder)
    {
        // Step 1: Go to https://developers.facebook.com/, and create a new app.
        // Step 2: Inside your app, go to Settings and set App Domains and Site URL to the root of your website e.g. http://myproject.uat.co
        // Step 3: Go to Apps Dashboard, copy App ID and set it to web.config key 'Authentication:Facebook:AppID'
        // Step 4: From Apps Dashboard, copy App Secret and set it to web.config key 'Authentication:Facebook:AppSecret'

        //authenticationBuilder.AddFacebook(configureOptions =>
        //{
        //    configureOptions.AppId = Config.Get("Authentication:Facebook.AppID");
        //    configureOptions.AppSecret = Config.Get("Authentication:Facebook.AppSecret");
        //});
    }

    private static void EnableGoogle(AuthenticationBuilder authenticationBuilder)
    {
        // Step 1: Go to https://console.developers.google.com/ and create a new app.
        // Step 2: Inside your app go to 'APIs & auth > APIs' screen and enable "Google+ API" and "Google+ Domains API".
        // Step 3: Go to 'APIs & auth > Credentials' screen and set Redirect URLs to http://XXX/signin-google (XXX is your domain name).
        // Step 3: From same screen, copy Client Id and set it to web.config key 'Authentication:Google:ClientId'
        // Step 4: Also copy Client Secret and set it to web.config key 'Authentication:Google:ClientSecret'

        //authenticationBuilder.AddGoogle(configureOptions =>
        //{
        //    configureOptions.ClientId = Config.Get("Authentication:Google:ClientId");
        //    configureOptions.ClientSecret = Config.Get("Authentication:Google:ClientSecret");
        //});
    }

    //public void Configuration(IAppBuilder app)
    //{
    //    app.UseCookieAuthentication(new CookieAuthenticationOptions
    //    {
    //        LoginPath = new PathString(Config.Get("Authentication.LoginUrl")),
    //        SlidingExpiration = true,
    //        AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
    //        Provider = new CookieAuthenticationProvider
    //        {
    //            OnApplyRedirect = context => context.Response.Redirect(context.RedirectUri),
    //            OnResponseSignIn = context =>
    //            {
    //                var expiryTime = DateTime.UtcNow.Add(Config.Get<int>("Authentication.SessionTimeout").Minutes());
    //                context.Properties.ExpiresUtc = expiryTime;
    //            }
    //        }
    //    });

    //    app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

    //    RegisterExternalLoginCallback();

    //    EnableGoogle(app);

    //    EnableFacebook(app);
    //}

    //void RegisterExternalLoginCallback()
    //{
    //    var provider = new MSharp.Owin.Authentication.OwinAuthenticaionProvider();
    //    provider.ExternalLoginAuthenticated += StartupAuth_ExternalLoginAuthenticated;
    //    UserServices.AuthenticationProvider = provider;
    //}

    //void EnableGoogle(IAppBuilder app)
    //{
    //    // Step 1: Go to https://console.developers.google.com/ and create a new app.
    //    // Step 2: Inside your app go to 'APIs & auth > APIs' screen and enable "Google+ API" and "Google+ Domains API".
    //    // Step 3: Go to 'APIs & auth > Credentials' screen and set Redirect URLs to http://XXX/signin-google (XXX is your domain name).
    //    // Step 3: From same screen, copy Client Id and set it to web.config key 'Google.ClientId'
    //    // Step 4: Also copy Client Secret and set it to web.config key 'Google.clientSecret'
    //    app.UseGoogleAuthentication(
    //        clientId: Config.Get("Google.ClientId"),
    //        clientSecret: Config.Get("Google.clientSecret")
    //    );
    //}

    //void EnableFacebook(IAppBuilder app)
    //{
    //    // Step 1: Go to https://developers.facebook.com/, and create a new app.
    //    // Step 2: Inside your app, go to Settings and set App Domains and Site URL to the root of your website e.g. http://myproject.uat.co
    //    // Step 3: Go to Apps Dashboard, copy App ID and set it to web.config key 'Facebook.AppID'
    //    // Step 4: From Apps Dashboard, copy App Secret and set it to web.config key 'Facebook.AppSecret'
    //    var options = new FacebookAuthenticationOptions
    //    {
    //        AppId = Config.Get("Facebook.AppID"),
    //        AppSecret = Config.Get("Facebook.AppSecret")
    //    };

    //    options.Scope.Add("email");
    //    app.UseFacebookAuthentication(options);
    //}

    //void StartupAuth_ExternalLoginAuthenticated(object sender, ExternalLoginInfo info)
    //{
    //    if (!info.IsAuthenticated)
    //    {
    //        HttpContext.Current.Response.Redirect("/Login"); return;
    //    }

    //    var user = User.FindByEmail(info.Email);
    //    var error = string.Empty; ;

    //    if (info.Email.IsEmpty())
    //    {
    //        error = "no-email";
    //    }
    //    else if (user == null)
    //    {
    //        error = "not-registered";

    //        // TODO: If in your project you want to register user as well the uncomment this line and comment the above one
    //        // user = CreateUser(e, user);
    //    }
    //    else if (user.IsDeactivated)
    //    {
    //        error = "deactivated";
    //    }

    //    if (error.HasValue())
    //    {
    //        HttpContext.Current.Response.Redirect($"~/login?ReturnUrl=/login&email={info.Email}&provider={info.Issuer}&error={error}");
    //    }

    //    user.LogOn();

    //    HttpContext.Current.Response.Redirect("~/");
    //}

    ////static User CreateUser(ExternalLoginInfo info, User user)
    ////{
    ////    var nameWithSpaces = Regex.Replace(info.UserName, @"((?<=\p{Ll})\p{Lu}|\p{Lu}(?=\p{Ll}))", " $1").Trim();
    ////    var lastSpaceIndex = nameWithSpaces.LastIndexOf(' ');

    ////    var firstName = nameWithSpaces.Substring(0, lastSpaceIndex);
    ////    var lastName = nameWithSpaces.Substring(lastSpaceIndex);

    ////    throw new NotImplementedException("Creating user is not implemented.");

    ////    // EXAMPLE:

    ////   user = Database.Save(new MyUserType
    ////   {
    ////       Email = e.Email,
    ////       FirstName = firstName,
    ////       LastName = lastName,
    ////       Password = new Guid().ToString(),
    ////       Salt = new Guid().ToString()
    ////   });
    ////   return user;
    ////}
}