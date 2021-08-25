using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.Extend
{
    /// <summary>
    /// Abstract base class for content types.
    /// </summary>
    public abstract class ContentType
    {
        #region Properties
        /// <summary>
        /// Gets/sets the unique id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets/sets the optional title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets/sets the available regions.
        /// </summary>
        public IList<RegionType> Regions { get; set; }
        #endregion

        protected ContentType()
        {
            Regions = new List<RegionType>();
        }
    }
}
