using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.Builder.Attribute
{
    /// <summary>
    /// Attribute for marking a class as a page type.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class PageTypeAttribute: System.Attribute
    {
        /// <summary>
        /// Gets/sets the unique id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets/sets the optional title. 
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets/sets the optional route.
        /// </summary>
        public string Route { get; set; }
    }
}
