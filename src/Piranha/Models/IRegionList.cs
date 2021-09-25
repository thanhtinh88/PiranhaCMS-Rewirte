using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.Models
{
    /// <summary>
    /// Interface for accessing the meta data of a region list.
    /// </summary>
    public interface IRegionList
    {
        /// <summary>
        /// Gets/sets the page type id.
        /// </summary>
        public string TypeId { get; set; }

        /// <summary>
        /// Gets/sets the region id.
        /// </summary>
        public string RegionId { get; set; }

        /// <summary>
        /// Clears the list
        /// </summary>
        void Clear();

        /// <summary>
        /// Adds a new item to the region list
        /// </summary>
        /// <param name="item">The item</param>
        void Add(object item);
    }
}
