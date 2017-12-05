using Domain;
using Olive.Mvc;
using Olive.Security;

namespace Controllers
{
    // TODO: Uncomment this if you want to support JWT authentication, for example for WebAPI clients.
    //[JwtAuthenticate]

    public class BaseController : Olive.Mvc.Controller
    {
        /// <summary>Gets the user for the current HTTP request.</summary>
        public new User User => base.User?.Identity.Extract<User>() ?? new AnonymousUser();
    }
}

namespace ViewComponents
{
    public abstract class ViewComponent : Olive.Mvc.ViewComponent
    {
        /// <summary>Gets the user for the current HTTP request.</summary>
        public new User User => base.User?.Identity.Extract<User>() ?? new AnonymousUser();
    }
}