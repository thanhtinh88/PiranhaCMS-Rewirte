using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.EF.Data
{    
    public sealed class Page: IModel, ISlug, ICreated, IModified
    {
        #region Propreties
        /// <summary>
        /// Gets/sets the unique id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets/sets the content type id.
        /// </summary>
        public string TypeId { get; set; }

        /// <summary>
        /// Gets/sets the optional parent id.
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// Gets/sets the sort order.
        /// </summary>
        public int SortOrder { get; set; }

        /// <summary>
        /// Gets/sets the title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets/sets the optional navigation title.
        /// </summary>
        public string NavigationTitle { get; set; }

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
        /// Gets/sets the internal route used by the middleware.
        /// </summary>
        public string Route { get; set; }

        /// <summary>
        /// Gets/sets the published date.
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

        #region Navigation Properties
        /// <summary>
        /// Gets/sets the page type.
        /// </summary>
        public PageType Type { get; set; }

        /// <summary>
        /// Gets/sets the available fields.
        /// </summary>
        public IList<PageField> Fields { get; set; }
        #endregion

        public Page()
        {
            Fields = new List<PageField>();
        }
    }
}
