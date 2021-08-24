using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.Builder.Json
{
    /// <summary>
    /// A config file used by the builder.
    /// </summary>
    internal class ConfigFile
    {
        /// <summary>
        /// Gets/sets the filename.
        /// </summary>
        public string Filename { get; set; }

        /// <summary>
        /// Gets/sets if the file is optional.
        /// </summary>
        public bool Optional { get; set; }
    }
}
