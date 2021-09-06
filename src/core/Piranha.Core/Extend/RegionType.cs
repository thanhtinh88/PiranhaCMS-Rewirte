using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.Extend
{
    public sealed class RegionType
    {
        #region Properties
        /// <summary>
        /// Gets/sets the id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets/sets the optional title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets/sets if this region has a collection of values.
        /// </summary>
        public bool Collection { get; set; }

        /// <summary>
        /// Gets/sets the available fields.
        /// </summary>
        public IList<FieldType> Fields { get; set; }
        #endregion

        internal RegionType()
        {
            Fields = new List<FieldType>();
        }
    }
}
