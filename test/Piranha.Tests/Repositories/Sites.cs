using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Xunit;

namespace Piranha.Tests.Repositories
{
    public class Sites: BaseTests
    {
        #region Members
        private const string SITE_1 = "MyFirstSite";
        private const string SITE_2 = "MySecondSite";
        private const string SITE_3 = "MyThirdSite";
        private const string SITE_4 = "MyFourthSite";
        private const string SITE_5 = "MyFifthSite";
        private const string SITE_1_HOSTS = "mysite.com";

        private Guid SITE_1_ID = Guid.NewGuid();
        #endregion

        protected override void Init()
        {
            using (var api = new Api(options))
            {
                api.Sites.Save(new Data.Site()
                {
                    Id = SITE_1_ID,
                    InternalId = SITE_1,
                    Title = SITE_1,
                    HostNames = SITE_1_HOSTS,
                    IsDefault = true
                });

                api.Sites.Save(new Data.Site()
                {
                    InternalId = SITE_4,
                    Title = SITE_4
                });
                api.Sites.Save(new Data.Site()
                {
                    InternalId = SITE_5,
                    Title = SITE_5
                });

            }
        }

        protected override void Cleanup()
        {
            using (var api = new Api(options))
            {
                var sites = api.Sites.GetAll();
                foreach (var site in sites)
                {
                    api.Sites.Delete(site);
                }
            }
        }

        [Fact]
        public void Add()
        {
            using (var api = new Api(options))
            {
                var count = api.Sites.GetAll().Count();
                api.Sites.Save(new Data.Site()
                {
                    InternalId = SITE_2,
                    Title = SITE_2
                });

                Assert.Equal(count + 1, api.Sites.GetAll().Count());
            }
        }

        [Fact]
        public void AddDuplicateKey()
        {
            using (var api = new Api(options))
            {
                Assert.Throws<SqlException>(() => api.Sites.Save(new Data.Site()
                {
                    InternalId = SITE_1,
                    Title = SITE_1
                }));
            }
        }

        [Fact]
        public void GetAll()
        {
            using (var api = new Api(options))
            {
                var models = api.Sites.GetAll();

                Assert.NotNull(models);
                Assert.NotEmpty(models);
            }
        }

        [Fact]
        public void GetById()
        {
            using (var api = new Api(options))
            {
                var site = api.Sites.GetById(SITE_1_ID);

                Assert.NotNull(site);
                Assert.Equal(SITE_1, site.InternalId);
            }
        }

        [Fact]
        public void GetByInternalId()
        {
            using (var api = new Api(options))
            {
                var model = api.Sites.GetByInternalId(SITE_1);

                Assert.NotNull(model);
                Assert.Equal(SITE_1, model.InternalId);
            }
        }

        [Fact]
        public void GetDefault()
        {
            using (var api = new Api(options))
            {
                var model = api.Sites.GetDefault();

                Assert.NotNull(model);
                Assert.Equal(SITE_1, model.InternalId);
            }
        }

        [Fact]
        public void Update()
        {
            using (var api = new Api(options))
            {
                var model = api.Sites.GetById(SITE_1_ID);

                Assert.Equal(SITE_1_HOSTS, model.HostNames);

                model.HostNames = "Updated";
                api.Sites.Save(model);

                model = api.Sites.GetById(SITE_1_ID);
                Assert.Equal("Updated", model.HostNames);
            }
        }

        [Fact]
        public void Delete()
        {
            using (var api = new Api(options))
            {
                var count = api.Sites.GetAll().Count();
                var model = api.Sites.GetByInternalId(SITE_4);

                Assert.NotNull(model);
                api.Sites.Delete(model);

                Assert.Equal(count - 1, api.Sites.GetAll().Count());
            }
        }

        [Fact]
        public void DeleteById()
        {
            using (var api = new Api(options))
            {
                var count = api.Sites.GetAll().Count();
                var model = api.Sites.GetByInternalId(SITE_4);

                Assert.NotNull(model);
                api.Sites.Delete(model.Id);

                Assert.Equal(count - 1, api.Sites.GetAll().Count());
            }
        }
    }
}
