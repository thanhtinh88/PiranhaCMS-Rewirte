using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.Models
{
    public sealed class FieldType
    {
        /// <summary>
        /// Gets/sets the id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets/sets the optional title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets/sets the value type.
        /// </summary>
        public string Type { get; set; }
    }
}
