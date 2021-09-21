using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Piranha.Areas.Manager.Controllers
{
    public class ManagerAreaControllerBase : Controller
    {
        #region Properties
        #region Protected Properties
        /// <summary>
        /// The current api
        /// </summary>
        protected readonly IApi _api;
        #endregion
        #endregion

        public ManagerAreaControllerBase(IApi api)
        {
            _api = api;
        }
    }
}
