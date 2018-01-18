using Olive;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;

public static class AuthenticationBuilderExtensions
{
    // To obtain the keys See > https://github.com/Geeksltd/Olive/blob/master/Olive.Security/Config.md

    public static void AddSocialAuth(this AuthenticationBuilder auth)
    {
        #region Microsoft
        // auth.AddMicrosoftAccount(config =>
        // {
        //     config.ClientId = Config.Get("Authentication:Microsoft:ApplicationId");
        //     config.ClientSecret = Config.Get("Authentication:Microsoft:Password");
        // });
        #endregion

        #region Facebook
        // auth.AddFacebook(config =>
        // {
        //     config.AppId = Config.Get("Authentication:Facebook.AppID");
        //     config.AppSecret = Config.Get("Authentication:Facebook.AppSecret");
        // });
        #endregion

        #region Google
        // auth.AddGoogle(config =>
        // {
        //     config.ClientId = Config.Get("Authentication:Google:ClientId");
        //     config.ClientSecret = Config.Get("Authentication:Google:ClientSecret");
        // });
        #endregion
    }
}