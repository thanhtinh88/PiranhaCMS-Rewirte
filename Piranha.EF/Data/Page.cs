using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.EF.Data
{    
    public sealed class Page: Models.PageBase, IModel, ISlug, ICreated, IModified
    {
        #region Propreties

        /// <summary>
        /// Gets/sets the available fields.
        /// </summary>
        public IList<PageField> Fields { get; set; }
        #endregion

        #region Navigation Properties
        /// <summary>
        /// Gets/sets the page type.
        /// </summary>
        public PageType PageType { get; set; }
        #endregion

        public Page()
        {
            Fields = new List<PageField>();
        }
    }
}
