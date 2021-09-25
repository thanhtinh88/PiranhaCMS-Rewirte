using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.AttributeBuilder
{
    /// <summary>
    /// Attribute for marking a property as a region.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class RegionAttribute:System.Attribute
    {
        /// <summary>
        /// Gets/sets the optional title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets/sets the maximum number of items if this is a collection.
        /// </summary>
        public int Max { get; set; }

        /// <summary>
        /// Gets/sets the minimum number of items if this is a collection.
        /// </summary>
        public int Min { get; set; }
    }
}
