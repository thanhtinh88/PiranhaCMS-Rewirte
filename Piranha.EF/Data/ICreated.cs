using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.EF.Data
{
    /// <summary>
    /// Interface for data models tracking created date.
    /// </summary>
    public interface ICreated
    {
        /// <summary>
        /// Gets/sets the created date.
        /// </summary>
        DateTime Created { get; set; }
    }
}
