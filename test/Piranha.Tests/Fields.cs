
using Piranha.Extend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Piranha.Tests
{
    public class Fields: BaseTests
    {
        [Field(Name = "First")]
        public class MyFirstField : Extend.Fields.SimpleField<string> { }
        [Field(Name = "Second")]
        public class MySecondField : Extend.Fields.SimpleField<string> { }

        protected override void Init()
        {
            using (var api = new Api(options))
            {
                Piranha.App.Init(api);
                Piranha.App.Fields.Register<MyFirstField>();
            }
        }

        protected override void Cleanup()
        {
            
        }

        [Fact]
        public void GetByType()
        {
            var field = Piranha.App.Fields.GetByType(typeof(Extend.Fields.HtmlField));
            
            Assert.NotNull(field);
            Assert.Equal("Html", field.Name);
        }

        [Fact]
        public void GetByTypeName()
        {
            var field = Piranha.App.Fields.GetByType(typeof(Extend.Fields.HtmlField).FullName);

            Assert.NotNull(field);
            Assert.Equal("Html", field.Name);
        }

        [Fact]
        public void GetByShorthand()
        {
            var field = Piranha.App.Fields.GetByShorthand("Html");

            Assert.NotNull(field);
            Assert.Equal("Html", field.Name);
        }

        [Fact]
        public void Register()
        {
            var count = Piranha.App.Fields.Count();

            Piranha.App.Fields.Register<MySecondField>();

            Assert.Equal(count + 1, Piranha.App.Fields.Count());
        }

        [Fact]
        public void UnRegister()
        {
            var count = Piranha.App.Fields.Count();

            Piranha.App.Fields.UnRegister<MyFirstField>();

            Assert.Equal(count - 1, Piranha.App.Fields.Count());
        }

        [Fact]
        public void Enumerate()
        {
            foreach (var field in Piranha.App.Fields)
            {
                Assert.NotNull(field);
            }
        }
    }
}
