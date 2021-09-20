using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace Piranha.Models
{
    /// <summary>
    /// The client post model.
    /// </summary>
    public class PostArchive
    {
        #region Properties
        /// <summary>
        /// Gets/sets the unique id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets/sets the main title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets/sets the unique slug.
        /// </summary>
        public string Slug { get; set; }

        /// <summary>
        /// Gets/sets the optional meta keywords.
        /// </summary>
        public string MetaKeywords { get; set; }

        /// <summary>
        /// Gets/sets the optional meta description.
        /// </summary>
        public string MetaDescription { get; set; }

        /// <summary>
        /// Gets/sets the optionally requested year.
        /// </summary>
        public int? Year { get; set; }

        /// <summary>
        /// Gets/sets the optionally requested month.
        /// </summary>
        public int? Month { get; set; }

        /// <summary>
        /// Gets/sets the current page.
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Gets/sets the total number of pages available.
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// Gets/sets the internal route used by the middleware.
        /// </summary>
        public string Route { get; set; }

        /// <summary>
        /// Gets/sets the available regions.
        /// </summary>
        public IList<Post> Posts { get; set; }
        #endregion
        public PostArchive()
        {
            Posts = new List<Post>();
        }
    }
}
