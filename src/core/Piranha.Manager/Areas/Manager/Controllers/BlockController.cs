using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Piranha.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class BlockController : ManagerAreaControllerBase
    {

        public BlockController(IApi api): base(api) { }

        /// <summary>
        /// Gets the list view for the blocks.
        /// </summary>
        [Route("manager/blocks")]
        public ViewResult List()
        {
            return View();
        }
    }
}
