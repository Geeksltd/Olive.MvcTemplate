using System;
using Domain;
using Olive;

namespace Controllers
{
    public class BaseController : Olive.Mvc.Controller
    {
        /// <summary>Gets the user for the current HTTP request.</summary>
        public new User User => base.User.Extract().AwaitResult() ?? new AnonymousUser();
    }
}

namespace ViewComponents
{
    public abstract class ViewComponent : Olive.Mvc.ViewComponent
    {
        /// <summary>Gets the user for the current HTTP request.</summary>
        public new User User
        {
            get
            {
                var id = base.User?.Identity?.Name;
                return Database.GetOrDefault<User>(id).AwaitResult()
                    ?? new AnonymousUser();
            }
        }
    }
}