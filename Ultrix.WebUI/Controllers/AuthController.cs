using Microsoft.AspNetCore.Mvc;

namespace Ultrix.WebUI.Controllers
{
    [Route("Auth")]
    public class AuthController : Controller
    {
        //private readonly IUserService _userService;

        public AuthController(/*IUserService userService*/)
        {
           // _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}