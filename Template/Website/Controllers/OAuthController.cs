using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Olive;
using Olive.Web;

namespace Controllers
{
    public class OAuthController : Controller
    {
        readonly SignInManager<User> SignInManager;
        public OAuthController(SignInManager<User> signInManager) => SignInManager = signInManager;

        [HttpGet, Route("ExternalLoginCallback")]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            if (remoteError != null)
                return ShowError(string.Empty, $"Error from external provider: {remoteError}");

            var info = await SignInManager.GetExternalLoginInfoAsync();
            if (info == null) return Redirect("/login");

            var email = info.Principal.Claims.FirstOrDefault(c => c.Type.EndsWith("emailaddress"))?.Value;
            if (email.IsEmpty()) return ShowError(info.LoginProvider, "no-email");

            var user = await Domain.User.FindByEmail(email);
            if (user == null)
            {
                // TODO: If in your project you want to register user as well the uncomment this line and comment the above one
                // user = CreateUser(e, user);
                return ShowError(info.LoginProvider, "not-registered", email);
            }

            if (user.IsDeactivated) return ShowError(info.LoginProvider, "deactivated", email);

            user.LogOn();
            return Redirect("/login");
        }

        RedirectResult ShowError(string loginProvider, string errorKey, string email = null)
        {
            return Redirect($"/login?ReturnUrl=/login&email={email}&provider={loginProvider}&error={errorKey}");
        }
    }
}