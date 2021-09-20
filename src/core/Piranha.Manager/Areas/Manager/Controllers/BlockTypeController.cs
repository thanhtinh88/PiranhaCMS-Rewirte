using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Piranha.Manager.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class BlockTypeController : Controller
    {
        #region Members
        /// <summary>
        /// The current api.
        /// </summary>
        private IApi api;
        #endregion

        public BlockTypeController(IApi api)
        {
            this.api = api;
        }

        /// <summary>
        /// Gets the list view for the block types.
        /// </summary>
        [Route("manager/blocktypes")]
        public IActionResult List()
        {
            return View(App.BlockTypes);
        }

        /// <summary>
        /// Gets the edit view for the specified block type.
        /// </summary>
        [Route("manager/blocktype/{id}")]
        public IActionResult Edit(string id)
        {
            return View(App.BlockTypes.SingleOrDefault(t => t.Id.ToLower() == id.ToLower()));
        }
    }
}
