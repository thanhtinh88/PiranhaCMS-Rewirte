using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.Extend
{
    public sealed class FieldInfo
    {
        /// <summary>
        /// Gets/sets the field name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets/sets the optional shorthand.
        /// </summary>
        public string Shorthand { get; set; }

        /// <summary>
        /// Gets/sets the CLRType.
        /// </summary>
        public string CLRType { get; set; }

        /// <summary>
        /// Gets/sets the type.
        /// </summary>
        public Type Type { get; set; }
    }
}
