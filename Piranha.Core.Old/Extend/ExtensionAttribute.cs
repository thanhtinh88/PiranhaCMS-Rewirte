using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Piranha.Core.Extend
{
    /// <summary>
    /// Attribute for marking a class as an extension.
    /// </summary>
    public sealed class ExtensionAttribute:Attribute
    {
        /// <summary>
        /// Gets/sets the current extension types.
        /// </summary>
        public ExtensionType Types { get; set; }
    }
}
