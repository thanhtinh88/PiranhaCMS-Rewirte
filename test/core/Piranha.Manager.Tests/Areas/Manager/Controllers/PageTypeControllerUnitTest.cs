using Piranha.Areas.Manager.Controllers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.Manager.Tests.Areas.Manager.Controllers
{
    public class PageTypeControllerUnitTest: ManagerAreaControllerUnitTestBase<PageTypeController>
    {
        protected override PageTypeController SetupController()
        {
            return new PageTypeController(mockApi.Object);
        }
    }
}
