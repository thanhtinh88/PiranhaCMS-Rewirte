using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.Extend
{
    public sealed class AppField: AppDataItem
    {
        /// <summary>
        /// Gets/sets the display name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets/sets the shorthand name.
        /// </summary>
        public string Shorthand { get; set; }
    }
}
