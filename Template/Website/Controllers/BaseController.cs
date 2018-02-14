﻿using Domain;
using Olive.Mvc;
using Olive.Security;
using Olive.Web;
using Olive;

namespace Controllers
{
    // TODO: Uncomment this if you want to support JWT authentication, for example for WebAPI clients.
    //[JwtAuthenticate]

    public class BaseController : Olive.Mvc.Controller
    {
        /// <summary>Gets a Domain User object extracted from the current user principal.</summary>
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