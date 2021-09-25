using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Piranha.Tests.Utils
{
    public class Slug
    {
        [Fact]
        public void ToLowerCase()
        {
            Assert.Equal("mycamelcasestring", Piranha.Utils.GenerateSlug("MyCamelCaseString"));
        }

        [Fact]
        public void Trim()
        {
            Assert.Equal("trimmed", Piranha.Utils.GenerateSlug("   trimmed   "));
        }

        [Fact]
        public void ReplaceWhiteSpace()
        {
            Assert.Equal("no-whitespaces", Piranha.Utils.GenerateSlug("no whitespaces"));
        }

        [Fact]
        public void ReplaceDoubleDashed()
        {
            Assert.Equal("no-whitespaces", Piranha.Utils.GenerateSlug("no - whitespaces"));
        }

        [Fact]
        public void TrimDashes()
        {
            Assert.Equal("trimmed", Piranha.Utils.GenerateSlug("-trimmed-"));
        }

        [Fact]
        public void RemoveSwedishCharacters()
        {
            Assert.Equal("aaoaao", Piranha.Utils.GenerateSlug("åäöÅÄÖ"));
        }

        [Fact]
        public void RemoveHyphenCharacters()
        {
            Assert.Equal("aaooeeiiaaooeeii", Piranha.Utils.GenerateSlug("áàóòéèíìÁÀÓÒÉÈÍÌ"));
        }
    }
}
