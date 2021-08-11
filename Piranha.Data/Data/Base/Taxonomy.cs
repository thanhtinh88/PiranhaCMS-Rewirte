using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.Data.Data.Base
{
    /// <summary>
    /// Base class for all taxonomies.
    /// </summary>
    public abstract class Taxonomy: IModel, ISlug
    {
        #region Properties
        /// <summary>
        /// Gets/sets the unique id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets/sets the display title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets/sets the unique slug.
        /// </summary>
        public string Slug { get; set; }
        #endregion
    }
}
