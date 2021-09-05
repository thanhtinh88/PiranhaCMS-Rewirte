using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Piranha.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class PageTypeController : Controller
    {
        #region members
        /// <summary>
        /// The current api.
        /// </summary>
        private IApi api; 
        #endregion

        public PageTypeController(IApi api)
        {
            this.api = api;
        }

        /// <summary>
        /// Gets the list view for the page types.
        /// </summary>
        [Route("manager/pagetypes")]
        public IActionResult List()
        {
            return View(App.PageTypes);
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
