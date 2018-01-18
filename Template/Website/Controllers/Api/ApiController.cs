using Domain;
using Olive.Mvc;
using Olive.Security;
using Olive.Web;

namespace Controllers
{
    [JwtAuthenticate]
    public class ApiController : BaseController
    {
    }
}