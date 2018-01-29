using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Website.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/Test")]
    public class TestController : Controller
    {
        [HttpGet]
        public Tst Get()
        {
            return new Tst();
        }
    }
    public class Tst
    {
        public Tst()
        {
            this.Message = "Hello world";
        }
        public string Message { get; set; }
    }
}