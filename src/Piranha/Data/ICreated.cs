using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.Data
{
    /// <summary>
    /// Interface for models tracking created date.
    /// </summary>
    public interface ICreated
    {
        /// <summary>
        /// Gets/sets the created date.
        /// </summary>
        public DateTime Created { get; set; }
    }
}
