using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Piranha.Tests.Utils
{
    public class Subset
    {
        [Fact]
        public void WithStart()
        {
            var array = new int[] { 1, 2, 3, 4, 5, 6 };

            array = array.Subset(2);

            Assert.Equal(4, array.Length);
            Assert.Equal(new int[] { 3, 4, 5, 6 }, array);
        }

        [Fact]
        public void WithStartAndEnd()
        {
            var array = new int[] { 1, 2, 3, 4, 5, 6 };

            array = array.Subset(2, 2);

            Assert.Equal(2, array.Length);
            Assert.Equal(new int[] { 3, 4 }, array);
        }

        [Fact]
        public void LenghtExceeded()
        {
            var array = new int[] { 1, 2, 3, 4, 5, 6 };

            array = array.Subset(2, 5);

            Assert.Equal(4, array.Length);
            Assert.Equal(new int[] { 3, 4, 5, 6 }, array);
        }
    }
}
