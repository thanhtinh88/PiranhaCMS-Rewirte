using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.Extend
{
    public class FieldAttribute: Attribute
    {
        /// <summary>
        /// Gets/sets the display name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets/sets the optional shorthand for type declaration.
        /// </summary>
        public string Shorthand { get; set; }
    }
}
