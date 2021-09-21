using Microsoft.AspNetCore.Mvc;
using Moq;
using Piranha.Areas.Manager.Controllers;
using Piranha.Areas.Manager.Models;
using Piranha.Extend;
using Piranha.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Piranha.Manager.Tests.Areas.Manager.Controllers
{
    public class PageControllerUnitTest: ManagerAreaControllerUnitTestBase<PageController>
    {
        #region Properties
        #region Private properties
        private const int NUM_PAGE_TYPES = 5;
        private List<PageType> pageTypes;
        #endregion
        #endregion


        #region Test setup
        protected override Mock<IApi> SetupApi()
        {
            var api = new Mock<IApi>();

            GeneratePageTypes();
            api.Setup(a => a.BlockTypes.Get()).Returns(new List<BlockType>());
            api.Setup(a => a.PageTypes.Get()).Returns(pageTypes);

            return api;
        }

        private void GeneratePageTypes()
        {
            pageTypes = new List<PageType>();
            for (int i = 0; i < NUM_PAGE_TYPES; i++)
            {
                pageTypes.Add(new PageType
                {
                    Id = $"{i}",
                    Title = $"Page Type {i}"
                });
            }
        }

        protected override PageController SetupController()
        {
            return new PageController(mockApi.Object);
        }
        #endregion


        [Fact]
        public void ListResultIsnotNullAndhasCorrectNumberPageTypes()
        {
            #region Arrange
            mockApi.Setup(a => a.Sitemap.Get(false)).Returns(new List<SitemapItem>());
            #endregion Arrange

            #region Act
            ViewResult result = controller.List();
            #endregion Act

            #region Assert
            Assert.NotNull(result);
            PageListModel Model = result.Model as PageListModel;
            Assert.NotNull(Model);
            Assert.Equal(pageTypes.Count, Model.PageTypes.Count);
            #endregion Assert
        }
    }
}
