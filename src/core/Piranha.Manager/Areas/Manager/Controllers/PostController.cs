using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Piranha.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class PostController : ManagerAreaControllerBase
    {
        public PostController(IApi api): base(api) { }

        [Route("manager/posts")]
        public IActionResult List()
        {
            return View();
        }
    }
}
