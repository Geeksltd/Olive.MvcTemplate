namespace Controllers
{
    using System;
    using Domain;
    using System.Linq;
    using System.ComponentModel;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using Olive.Entities;
    using Olive.Mvc;
    using Microsoft.AspNetCore.Http;
    using Olive;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Olive.Microservices;

    public class ApiController : BaseController
    {
        [HttpGet]
        [Route("userinfo/{email}")]
        public async Task<IActionResult> GetUserInfo(string email)
        {
            //User.IsTrustedService();

            if (email.IsEmpty()) return BadRequest();

            var user = await Employee.FindByEmail(email) as Domain.Employee;
            if (user == null) return NoContent();

            var result = new
            {
                Email = user.Email,
                ID = user.ID,
                DisplayName = user.Name,
                Roles = await user.GetRoles()
            };

            return Json(result);
        }
    }
}
