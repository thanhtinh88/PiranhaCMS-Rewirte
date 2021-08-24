using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.Models
{
    public class CategoryModel: Category
    {
        #region Properties
        /// <summary>
        /// Gets/sets the archive title.
        /// </summary>
        public string ArchiveTitle { get; set; }

        /// <summary>
        /// Gets/sets the archive meta keywords.
        /// </summary>
        public string ArchiveKeyword { get; set; }

        /// <summary>
        /// Gets/sets the archive meta description.
        /// </summary>
        public string ArchiveDescription { get; set; }

        /// <summary>
        /// Gets/sets the archive route.
        /// </summary>
        public string ArchiveRoute { get; set; }
        #endregion
    }
}
