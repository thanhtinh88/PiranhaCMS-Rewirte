using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.AttributeBuilder
{
    /// <summary>
    /// Abstract class for building content types.
    /// </summary>
    public abstract class ContentTypeAttribute: System.Attribute
    {
        /// <summary>
        /// Gets/sets the unique id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets/sets the optional title. 
        /// </summary>
        public string Title { get; set; }
    }
}
