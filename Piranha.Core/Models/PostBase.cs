﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.Models
{
    /// <summary>
    /// Posts are used to create content without a fixed position in the sitemap.
    /// </summary>
    public abstract class PostBase
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

        #endregion

    }
}
