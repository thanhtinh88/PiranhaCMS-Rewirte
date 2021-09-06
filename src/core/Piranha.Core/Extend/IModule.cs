using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.Extend
{
    /// <summary>
    /// Interface for defining a Piranha module.
    /// </summary>
    public interface IModule
    {
        /// <summary>
        /// Initializes the module.
        /// </summary>
        void Init();
    }
}
