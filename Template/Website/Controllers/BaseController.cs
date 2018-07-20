using Domain;
using Olive.Mvc;
using Olive.Security;
using Olive.Web;
using Olive;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    // TODO: Uncomment this if you want to support JWT authentication, for example for WebAPI clients.
    //[JwtAuthenticate]

    public class BaseController : Olive.Mvc.Controller
    {
		// TODO: Uncomment this if you want to use ApiClient
		// ApiClient.FallBack.Handle(arg => Notify(arg.FriendlyMessage, false));
		
        /// <summary>Gets a Domain User object extracted from the current user principal.</summary>
        [NonAction]
        public User GetUser() => User.Extract<User>();
    }
}

namespace ViewComponents
{
    public abstract class ViewComponent : Olive.Mvc.ViewComponent
    {
        /// <summary>Gets a Domain User object extracted from the current user principal.</summary>
        public User GetUser() => Context.Current.Http().User.Extract<User>();
    }
}