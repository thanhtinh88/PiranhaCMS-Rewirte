using Moq;
using Piranha.Areas.Manager.Controllers;
using Piranha.Extend;
using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.Manager.Tests.Areas.Manager.Controllers
{
    public abstract class ManagerAreaControllerUnitTestBase<TControlelr>
        where TControlelr: ManagerAreaControllerBase
    {
        #region Properties
        #region Protected Properies
        protected readonly TControlelr controller;
        protected readonly Mock<IApi> mockApi;
        #endregion
        #endregion

        #region Test initialize
        public ManagerAreaControllerUnitTestBase()
        {
            mockApi = SetupApi();
            controller = SetupController();
            App.Init(mockApi.Object, IncludedModules());
        }

        protected virtual Mock<IApi> SetupApi()
        {
            var api = new Mock<IApi>();
            api.Setup(a => a.PageTypes.Get()).Returns(new List<PageType>());
            api.Setup(a => a.BlockTypes.Get()).Returns(new List<BlockType>());
            return api;
        }

        protected abstract TControlelr SetupController();

        protected virtual IModule[] IncludedModules()
        {
            return null;
        }
        #endregion
    }
}
