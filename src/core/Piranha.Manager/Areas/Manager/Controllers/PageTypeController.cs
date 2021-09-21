using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Piranha.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class PageTypeController : ManagerAreaControllerBase
    {
        public PageTypeController(IApi api) : base(api) { }

        /// <summary>
        /// Gets the list view for the page types.
        /// </summary>
        [Route("manager/pagetypes")]
        public IActionResult List()
        {
            return View(App.PageTypes);
        }
    }
}
