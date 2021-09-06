using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.EF.Data
{
    /// <summary>
    /// Interface for data models with a unique slug.
    /// </summary>
    public interface ISlug
    {
        /// <summary>
        /// Gets/sets the display title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets/sets the unique slug.
        /// </summary>
        public string Slug { get; set; }
    }
}
