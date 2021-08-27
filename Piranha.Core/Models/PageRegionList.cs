﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.Models
{
    /// <summary>
    /// Region list for dynamic page models.
    /// </summary>
    /// <typeparam name="T">The item type</typeparam>
    public class PageRegionList<T> : List<T>, IPageRegionList
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
        /// Creates a new item instance
        /// </summary>
        /// <returns>The new item</returns>
        public T Create()
        {
            return (T)PageModel.CreateRegion(TypeId, RegionId);
        }

        /// <summary>
        /// Creates and adds a new item instance to the list.
        /// </summary>
        /// <returns>The new item</returns>
        public T CreateAndAdd()
        {
            var item = Create();
            Add(item);
            return item;
        }
    }
}
