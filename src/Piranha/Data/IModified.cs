using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.Data
{
    /// <summary>
    /// Interface for models tracking last modification date.
    /// </summary>
    public interface IModified
    {
        /// <summary>
        /// Gets/sets the last modification date.
        /// </summary>
        DateTime LastModified { get; set; }
    }
}
