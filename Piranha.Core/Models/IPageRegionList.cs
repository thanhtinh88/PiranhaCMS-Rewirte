using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.Models
{
    /// <summary>
    /// Interface for accessing the meta data of a region list.
    /// </summary>
    public interface IPageRegionList
    {
        /// <summary>
        /// Gets/sets the page type id.
        /// </summary>
        string TypeId { get; set; }

        /// <summary>
        /// Gets/sets the region id.
        /// </summary>
        string RegionId { get; set; }
    }
}
