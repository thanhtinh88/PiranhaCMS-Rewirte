using Piranha.Areas.Manager.Controllers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.Manager.Tests.Areas.Manager.Controllers
{
    public class PostControllerUnitTest: ManagerAreaControllerUnitTestBase<PostController>
    {
        protected override PostController SetupController()
        {
            return new PostController(mockApi.Object);
        }
    }
}
