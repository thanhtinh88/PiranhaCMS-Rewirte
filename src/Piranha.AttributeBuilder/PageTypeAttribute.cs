using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.AttributeBuilder
{
    /// <summary>
    /// Attribute for marking a class as a page type.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class PageTypeAttribute: ContentTypeAttribute
    {
        /// <summary>
        /// Gets/sets the optional route.
        /// </summary>
        public string Route { get; set; }
    }
}
