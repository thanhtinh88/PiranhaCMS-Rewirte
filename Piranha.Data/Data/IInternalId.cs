using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.Data.Data
{
    /// <summary>
    /// Interface for data models with a unique internal id.
    /// </summary>
    public interface IInternalId
    {
        /// <summary>
        /// Gets/sets the display name.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets/sets the unique internal id.
        /// </summary>
        string InternalId { get; set; }
    }
}
