using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Piranha.Tests
{
    public class App: BaseTests
    {
        protected override void Init()
        {
            using (var api = new Api(options))
            {
                Piranha.App.Init(api);
            }
        }

        protected override void Cleanup()
        {
        }

        [Fact]
        public void AppInit()
        {
            using (var api = new Api(options))
            {
                Piranha.App.Init(api);
            }
        }

        [Fact]
        public void Markdown()
        {
            var str = "# This is the title\n" + "This is the body";
            str = Piranha.App.Markdown.Transform(str).Replace("\n", "");

            Assert.Equal("<h1>This is the title</h1><p>This is the body</p>", str);
        }

        [Fact]
        public void Fields()
        {
            Assert.NotNull(Piranha.App.Fields);
            Assert.NotEmpty(Piranha.App.Fields);
        }

        [Fact]
        public void Mapper()
        {
            Assert.NotNull(Piranha.App.Mapper);
        }

        [Fact]
        public void PropertyBindings()
        {
            Assert.NotNull(Piranha.App.PropertyBindings);
        }
    }
}
