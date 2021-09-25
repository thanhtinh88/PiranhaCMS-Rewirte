using Piranha.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Xunit;

namespace Piranha.AttributeBuilder.Tests
{
    public class AttributeBuilder: IDisposable
    {
        #region Members
        protected Action<DbBuilder> options = o =>
        {
            o.Connection = new SqlConnection("data source=(localdb)\\MSSQLLocalDB;initial catalog=piranha.dapper.tests;integrated security=true");
            o.Migrate = true;
        };
        #endregion

        #region Inner classes
        [PageType(Id = "Simple", Title = "Simple Page Type")]
        public class SimplePageType
        {
            [Region]
            public Extend.Fields.TextField Body { get; set; }
        }

        [PageType(Id = "Complex", Title = "Complex Page Type", Route ="/complex")]
        public class ComplexPageType
        {
            public class BodyRegion
            {
                [Field]
                public Extend.Fields.TextField Title { get; set; }
                [Field]
                public Extend.Fields.TextField Body { get; set; }
            }

            [Region(Title = "Intro", Min = 3, Max = 6)]
            public IList<Extend.Fields.TextField> Slider { get; set; }

            [Region(Title = "Main Content")]
            public BodyRegion Content { get; set; }
        }
        #endregion

        public AttributeBuilder()
        {
            using (var api = new Api(options))
            {
                App.Init(api);
            }
        }

        [Fact]
        public void AddSimpe()
        {
            using (var api = new Api(options))
            {
                var builder = new PageTypeBuilder(api)
                    .AddType(typeof(SimplePageType));
                builder.Build();

                var type = api.PageTypes.GetById("Simple");
                Assert.NotNull(type);
                Assert.Equal(1, type.Regions.Count);
                Assert.Equal(1, type.Regions[0].Fields.Count);
            }
        }

        [Fact]
        public void AddComplex()
        {
            using (var api = new Api(options))
            {
                var builder = new PageTypeBuilder(api)
                    .AddType(typeof(ComplexPageType));
                builder.Build();

                var type = api.PageTypes.GetById("Complex");

                Assert.NotNull(type);
                Assert.Equal(2, type.Regions.Count);

                Assert.Equal("Slider", type.Regions[0].Id);
                Assert.Equal("Intro", type.Regions[0].Title);
                Assert.True(type.Regions[0].Collection);
                Assert.Equal(1, type.Regions[0].Fields.Count);

                Assert.Equal("Content", type.Regions[1].Id);
                Assert.Equal("Main Content", type.Regions[1].Title);
                Assert.False(type.Regions[1].Collection);
                Assert.Equal(2, type.Regions[1].Fields.Count);
            }
        }

        public void Dispose()
        {
            using (var api = new Api(options))
            {
                var types = api.PageTypes.GetAll();
                foreach (var type in types)
                {
                    api.PageTypes.Delete(type);
                }
            }
        }
    }
}
