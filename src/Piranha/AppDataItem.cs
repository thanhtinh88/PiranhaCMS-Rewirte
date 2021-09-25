using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha
{
    /// <summary>
    /// An item in an app data list.
    /// </summary>
    public class AppDataItem
    {
        /// <summary>
        /// Gets/sets the type.
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// Gets/sets the full type name.
        /// </summary>
        public string TypeName { get; set; }
    }
}
