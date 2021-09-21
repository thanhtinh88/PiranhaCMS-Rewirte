﻿using Microsoft.AspNetCore.Mvc;
using Piranha.Areas.Manager.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Piranha.Manager.Tests.Areas.Manager.Controllers
{
    public class BlockControllerUnitTest: ManagerAreaControllerUnitTestBase<BlockController>
    {
        protected override BlockController SetupController()
        {
            return new BlockController(mockApi.Object);
        }

        [Fact]
        public void ListViewResultIsNotNull()
        {
            #region Arrange

            #endregion

            #region Act
            ViewResult result = controller.List() as ViewResult;
            #endregion

            #region Assert
            Assert.NotNull(result);
            #endregion
        }
    }
}
