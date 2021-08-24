using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.EF.Data
{
    /// <summary>
    /// Categories are used to organize posts.
    /// </summary>
    public sealed class Category: Models.Category, IModel, ISlug, ICreated, IModified
    {
        #region Properties
        /// <summary>
        /// Gets/sets the optional archive title.
        /// </summary>
        public string ArchiveTitle { get; set; }

        /// <summary>
        /// Gets/sets the archive meta keywords.
        /// </summary>
        public string ArchiveKeywords { get; set; }

        /// <summary>
        /// Gets/sets the archive meta description.
        /// </summary>
        public string ArchiveDescription { get; set; }

        /// <summary>
        /// Gets/sets the optional archive route.
        /// </summary>
        public string ArchiveRoute { get; set; }

        /// <summary>
        /// Gets/sets the created date.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Gets/sets the last modification date.
        /// </summary>
        public DateTime LastModified { get; set; }
        #endregion
    }
}
