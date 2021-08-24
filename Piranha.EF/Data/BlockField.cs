using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.EF.Data
{
    public sealed class BlockField: IModel
    {
        #region Properties
        /// <summary>
        /// Gets/sets the unique id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets/sets the block id.
        /// </summary>
        public Guid BlockId { get; set; }

        /// <summary>
        /// Gets/sets the region id.
        /// </summary>
        public Guid RegionId { get; set; }

        /// <summary>
        /// Gets/sets the field id.
        /// </summary>
        public string FieldId { get; set; }

        /// <summary>
        /// Gets/sets the sort order.
        /// </summary>
        public int SortOrder { get; set; }

        /// <summary>
        /// Gets/sets the sort order of the value.
        /// </summary>
        public string CLRType { get; set; }

        /// <summary>
        /// Gets/sets the JSON serialized value.
        /// </summary>
        public string Value { get; set; }
        #endregion

        #region Navigation Properties
        /// <summary>
        /// Gets/sets the page
        /// </summary>
        public Block Block { get; set; }

        #endregion
    }
}
