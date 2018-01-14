// Generated M# Preview File
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;
using Olive;
using Olive.Entities;
using Olive.Mvc;
using Olive.Security;
using Olive.Web;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using vm = ViewModel;

namespace ViewComponents
{
    public partial class Footer : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(object settings)
        {
            var info = Bind<vm.Footer>(settings);
            
            return View(info);
        }
    }
}

namespace Controllers
{
    public partial class FooterViewComponentActions : BaseController
    {
        [HttpPost, Route("Footer/Logout")]
        public async Task<ActionResult> Logout(vm.Footer info)
        {
            if (!(User.IsInRole("User")))
                return new UnauthorizedResult();
            await OAuth.Instance.LogOff();
            
            return Redirect(Url.Index("Login"));
        }
    }
}

namespace ViewModel
{
    public partial class Footer : IViewModel
    {
    }
}