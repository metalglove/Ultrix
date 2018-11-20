using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Ultrix.Presentation.Controllers
{
    public class CollectionController : Controller
    {
        [Route("Collections")]
        public IActionResult Collections()
        {
            return View("Collections");
        }
    }
}