using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Piranha.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class BlockController : Controller
    {
        #region Members
        /// <summary>
        /// The current api.
        /// </summary>
        private IApi api;
        #endregion

        public BlockController(IApi api)
        {
            this.api = api;
        }

        /// <summary>
        /// Gets the list view for the blocks.
        /// </summary>
        [Route("manager/blocks")]
        public IActionResult List()
        {
            return View();
        }
    }
}
