using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Piranha.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class BlockTypeController : ManagerAreaControllerBase
    {

        public BlockTypeController(IApi api) : base(api)
        {

        }

        /// <summary>
        /// Gets the list view for the block types.
        /// </summary>
        [Route("manager/blocktypes")]
        public ViewResult List()
        {
            return View(App.BlockTypes);
        }

        /// <summary>
        /// Gets the edit view for the specified block type.
        /// </summary>
        [Route("manager/blocktype/{id}")]
        public ViewResult Edit(string id)
        {
            return View(App.BlockTypes.SingleOrDefault(t => t.Id.ToLower() == id.ToLower()));
        }
    }
}
