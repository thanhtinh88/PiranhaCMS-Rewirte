using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.Builder.Attribute
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FieldAttribute: System.Attribute
    {
        /// <summary>
        /// Gets/Sets the optional title.
        /// </summary>
        public string Title { get; set; }
    }
}
