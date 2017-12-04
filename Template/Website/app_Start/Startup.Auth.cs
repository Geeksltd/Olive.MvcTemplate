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

        //authenticationBuilder.AddMicrosoftAccount(configureOptions =>
        //{
        //    configureOptions.ClientId = Config.Get("Authentication:Microsoft:ApplicationId");
        //    configureOptions.ClientSecret = Config.Get("Authentication:Microsoft:Password");
        //});
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
}