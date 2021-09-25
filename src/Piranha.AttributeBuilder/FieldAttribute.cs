using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.AttributeBuilder
{
    /// <summary>
    /// Attribute for marking a property as a field.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class FieldAttribute: System.Attribute
    {
        /// <summary>
        /// Gets/sets the optional title.
        /// </summary>
        public string Title { get; set; }
    }
}
