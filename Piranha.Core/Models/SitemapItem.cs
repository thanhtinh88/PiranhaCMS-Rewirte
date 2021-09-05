using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.Models
{
    public class SitemapItem
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
        /// Gets/sets the optional navigation title.
        /// </summary>
        public string NavigationTitle { get; set; }

        /// <summary>
        /// Gets/sets the unique permalink.
        /// </summary>
        public string Permalink { get; set; }

        /// <summary>
        /// Gets/sets the created date.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Gets/sets the last modification date.
        /// </summary>
        public DateTime LastModified { get; set; }

        /// <summary>
        /// Gets/sets the available child items.
        /// </summary>
        public IList<SitemapItem> Items { get; set; }
        #endregion

        public SitemapItem()
        {
            Items = new List<SitemapItem>();
        }
    }
}
