using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Piranha.Core.Extend
{
    /// <summary>
    /// Interface for all extensions.
    /// </summary>
    public interface IExtension
    {
        /// <summary>
        /// Gets the value for the client model.
        /// </summary>
        /// <returns>The value</returns>
        object GetValue();
    }
}
