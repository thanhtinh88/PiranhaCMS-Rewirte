using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Piranha.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class PostController : Controller
    {
        #region Members
        /// <summary>
        /// The current api.
        /// </summary>
        private IApi api; 
        #endregion

        public PostController(IApi api)
        {
            this.api = api;
        }

        [Route("manager/posts")]
        public IActionResult List()
        {
            return View();
        }

        /// <summary>
        /// Disposes the controller and its resources.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            api.Dispose();
            base.Dispose(disposing);
        }
    }
}
