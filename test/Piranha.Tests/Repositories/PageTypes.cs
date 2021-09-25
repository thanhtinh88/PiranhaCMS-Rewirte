using Piranha.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Piranha.Tests.Repositories
{
    [Collection("Integration tests")]
    public class PageTypes: BaseTests
    {
        #region Members
        private List<PageType> pageTypeSetups = new List<PageType>()
        {
            new PageType() {
                Id = "MyFirstType",
                Regions = new List<RegionType>() {
                    new RegionType() {
                        Id = "Body",
                        Fields = new List<FieldType>() {
                            new FieldType() {
                                Id = "Default",
                                Type = "Html"
                            }
                        }
                    }
                }
            },
            new PageType() {
                Id = "MySecondType",
                Regions = new List<RegionType>() {
                    new RegionType() {
                        Id = "Body",
                        Fields = new List<FieldType>() {
                            new FieldType() {
                                Id = "Default",
                                Type = "Text"
                            }
                        }
                    }
                }
            },
            new PageType() {
                Id = "MyThirdType",
                Regions = new List<RegionType>() {
                    new RegionType() {
                        Id = "Body",
                        Fields = new List<FieldType>() {
                            new FieldType() {
                                Id = "Default",
                                Type = "Image"
                            }
                        }
                    }
                }
            },
            new PageType() {
                Id = "MyFourthType",
                Regions = new List<RegionType>() {
                    new RegionType() {
                        Id = "Body",
                        Fields = new List<FieldType>() {
                            new FieldType() {
                                Id = "Default",
                                Type = "String"
                            }
                        }
                    }
                }
            },
            new PageType() {
                Id = "MyFifthType",
                Regions = new List<RegionType>() {
                    new RegionType() {
                        Id = "Body",
                        Fields = new List<FieldType>() {
                            new FieldType() {
                                Id = "Default",
                                Type = "Text"
                            }
                        }
                    }
                }
            }
        };
        #endregion

        protected override void Init()
        {
            using (var api = new Api(options))
            {
                api.PageTypes.Save(pageTypeSetups[0]);
                api.PageTypes.Save(pageTypeSetups[3]);
                api.PageTypes.Save(pageTypeSetups[4]);
            }
        }

        protected override void Cleanup()
        {
            using (var api = new Api(options))
            {
                var pageTypes = api.PageTypes.GetAll();

                foreach (var p in pageTypes)
                {
                    api.PageTypes.Delete(p);
                }
            }
        }

        [Fact]
        public void Add()
        {
            using (var api = new Api(options))
            {
                api.PageTypes.Save(pageTypeSetups[1]);
            }
        }

        [Fact]
        public void GetAll()
        {
            using (var api = new Api(options))
            {
                var models = api.PageTypes.GetAll();

                Assert.NotNull(models);
                Assert.NotEmpty(models);
            }
        }

        [Fact]
        public void GetById()
        {
            using (var api = new Api(options))
            {
                var model = api.PageTypes.GetById(pageTypeSetups[0].Id);

                Assert.NotNull(model);
                Assert.Equal(pageTypeSetups[0].Regions[0].Fields[0].Id, model.Regions[0].Fields[0].Id);
            }
        }

        [Fact]
        public void Update()
        {
            using (var api = new Api(options))
            {
                var model = api.PageTypes.GetById(pageTypeSetups[0].Id);
                Assert.Null(model.Title);
                model.Title = "Update";
                api.PageTypes.Save(model);
                model = api.PageTypes.GetById(pageTypeSetups[0].Id);
                Assert.Equal("Update", model.Title);
            }
        }

        [Fact]
        public void Delete()
        {
            using (var api = new Api(options))
            {
                var model = api.PageTypes.GetById(pageTypeSetups[4].Id);
                Assert.NotNull(model);
                api.PageTypes.Delete(model);
            }
        }

        [Fact]
        public void DeleteById()
        {
            using (var api = new Api(options))
            {
                var model = api.PageTypes.GetById(pageTypeSetups[4].Id);
                Assert.NotNull(model);
                api.PageTypes.Delete(model.Id);
                model = api.PageTypes.GetById(pageTypeSetups[4].Id);
                Assert.Null(model);
            }
        }
    }
}
