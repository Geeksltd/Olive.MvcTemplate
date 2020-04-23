using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Olive;
using Olive.Web;

namespace Controllers
{
    public class OAuthController : Controller
    {
        [HttpGet, Route("ExternalLoginCallback")]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            if (remoteError != null)
                return await ShowError(string.Empty, $"Error from external provider: {remoteError}");

            var info = await HttpContext.AuthenticateAsync();
            if (info == null || !info.Succeeded) return Redirect("/login");

            var issuer = info.Principal.GetFirstIssuer();
            var email = info.Principal.GetEmail();
            if (email.IsEmpty()) return await ShowError(issuer, "no-email");

            var user = await Domain.User.FindByEmail(email);
            if (user == null)
            {
                // TODO: If in your project you want to register user as well the uncomment this line and comment the above one
                // user = CreateUser(e, user);
                return await ShowError(issuer, "not-registered", email);
            }

            if (user.IsDeactivated)
            {
                return await ShowError(issuer, "deactivated", email);
            }
            else
            {
                await user.LogOn();
                return Redirect("/login");
            }
        }

        async Task<RedirectResult> ShowError(string loginProvider, string errorKey, string email = null)
        {
            await HttpContext.SignOutAsync();

            return Redirect($"/login?ReturnUrl=/login&email={email}&provider={loginProvider}&error={errorKey}");
        }
    }
}