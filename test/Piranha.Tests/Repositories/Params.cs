using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Xunit;

namespace Piranha.Tests.Repositories
{
    public class Params: BaseTests
    {
        #region Members
        private const string PARAM_1 = "MyFirstParam";
        private const string PARAM_2 = "MySecondParam";
        private const string PARAM_3 = "MyThirdParam";
        private const string PARAM_4 = "MyFourthParam";
        private const string PARAM_5 = "MyFifthParam";

        private Guid PARAM_1_ID = Guid.NewGuid();
        private string PARAM_1_VALUE = "My first value";

        private const string CUSTOM_STRING_PARAM = "CustomStringParam";
        private const string CUSTOM_BOOL_PARAM = "CustomBoolParam";
        private const string CUSTOM_OBJECT_PARAM = "CustomObjectParam";
        #endregion

        protected override void Init()
        {
            using (var api = new Api(options))
            {
                api.Params.Save(new Data.Param()
                {
                    Id = PARAM_1_ID,
                    Key = PARAM_1,
                    Value = PARAM_1_VALUE
                });

                api.Params.Save(new Data.Param()
                {
                    Key = PARAM_4
                });
                api.Params.Save(new Data.Param()
                {
                    Key = PARAM_5,
                });
            }
        }

        protected override void Cleanup()
        {
            using (var api = new Api(options))
            {
                var param = api.Params.GetAll();
                foreach (var p in param)
                {
                    api.Params.Delete(p);
                }
            }
        }

        [Fact]
        public void Add()
        {
            using (var api = new Api(options))
            {
                var count = api.Params.GetAll().Count();
                api.Params.Save(new Data.Param()
                {
                    Key = PARAM_2,
                    Value = "My Second value"
                });

                Assert.Equal(count + 1, api.Params.GetAll().Count());
            }
        }

        [Fact]
        public void AddDuplicateKey()
        {
            using (var api = new Api(options))
            {
                Assert.Throws<SqlException>(() => api.Params.Save(new Data.Param
                {
                    Key = PARAM_1,
                    Value = "My duplicate value"
                }));
            }
        }

        [Fact]
        public void GetAll()
        {
            using (var api = new Api(options))
            {
                var models = api.Params.GetAll();

                Assert.NotNull(models);
                Assert.NotEmpty(models);
            }
        }

        [Fact]
        public void GetById()
        {
            using (var api = new Api(options))
            {
                var model = api.Params.GetById(PARAM_1_ID);

                Assert.NotNull(model);
                Assert.Equal(PARAM_1, model.Key);
            }
        }

        [Fact]
        public void GetByKey()
        {
            using (var api = new Api(options))
            {
                var model = api.Params.GetByKey(PARAM_1);

                Assert.NotNull(model);
                Assert.Equal(PARAM_1, model.Key);
            }
        }

        [Fact]
        public void Update()
        {
            using (var api = new Api(options))
            {
                var model = api.Params.GetById(PARAM_1_ID);

                Assert.Equal(PARAM_1_VALUE, model.Value);

                model.Value = "Updated";
                api.Params.Save(model);

                model = api.Params.GetById(PARAM_1_ID);

                Assert.Equal("Updated", model.Value);
            }
        }

        [Fact]
        public void Delete()
        {
            using (var api = new Api(options))
            {
                var count = api.Params.GetAll().Count();
                var model = api.Params.GetByKey(PARAM_4);

                Assert.NotNull(model);
                api.Params.Delete(model);

                Assert.Equal(count - 1, api.Params.GetAll().Count());
            }
        }

        [Fact]
        public void DeleteById()
        {
            using (var api = new Api(options))
            {
                var count = api.Params.GetAll().Count();
                var model = api.Params.GetByKey(PARAM_5);

                Assert.NotNull(model);
                api.Params.Delete(model.Id);

                Assert.Equal(count - 1, api.Params.GetAll().Count());
            }
        }
    }
}
