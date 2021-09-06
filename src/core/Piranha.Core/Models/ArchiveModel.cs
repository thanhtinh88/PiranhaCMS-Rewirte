using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.Models
{
    public class ArchiveModel
    {
        #region Properties
        /// <summary>
        /// Gets/sets the unique id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets/sets the archive title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets/sets the archive slug.
        /// </summary>
        public string Slug { get; set; }

        /// <summary>
        /// Gets/sets the meta keywords.
        /// </summary>
        public string MetaKeywords { get; set; }

        /// <summary>
        /// Gets/sets the meta description..
        /// </summary>
        public string MetaDescription { get; set; }

        /// <summary>
        /// Gets/sets the currently requested year.
        /// </summary>
        public int? Year { get; set; }

        /// <summary>
        /// Gets/sets the currently requested month.
        /// </summary>
        public int? Month { get; set; }

        /// <summary>
        /// Gets/sets the currently requested page.
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Gets/sets the total number of pages available.
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// Gets/sets the optional route.
        /// </summary>
        public string Route { get; set; }

        /// <summary>
        /// Gets/sets the available posts.
        /// </summary>
        public IList<PostModel> Posts { get; set; }
        #endregion

        public ArchiveModel()
        {
            Posts = new List<PostModel>();
        }
    }
}
