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
    public class Post
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
        /// Gets/sets the full permalink for the post.
        /// </summary>
        public string Permalink { get; set; }

        /// <summary>
        /// Gets/sets the optional meta keywords.
        /// </summary>
        public string MetaKeywords { get; set; }

        /// <summary>
        /// Gets/sets the optional meta description.
        /// </summary>
        public string MetaDescription { get; set; }

        /// <summary>
        /// Gets/sets the optional excerpt.
        /// </summary>
        public string Excerpt { get; set; }

        /// <summary>
        /// Gets/sets the main post body.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Gets/sets the internal route used by the middleware.
        /// </summary>
        public string Route { get; set; }

        /// <summary>
        /// Gets/sets the optional published date.
        /// </summary>
        public DateTime? Published { get; set; }

        /// <summary>
        /// Gets/sets the created date.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Gets/sets the last modification date.
        /// </summary>
        public DateTime LastModified { get; set; }

        /// <summary>
        /// Gets/sets the post category.
        /// </summary>
        public CategoryItem Category { get; set; }

        /// <summary>
        /// Gets/sets the available regions.
        /// </summary>
        public IList<Tag> Tags { get; set; }
        #endregion
        public Post()
        {
            Tags = new List<Tag>();
        }
    }
}
