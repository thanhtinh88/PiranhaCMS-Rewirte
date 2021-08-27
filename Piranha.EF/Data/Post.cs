using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.EF.Data
{
    public class Post: Models.PostBase, IModel, ISlug, ICreated,IModified
    {
        #region Properties

        /// <summary>
        /// Gets/sets the category id.
        /// </summary>
        public Guid CategoryId { get; set; }
        #endregion

        #region Navigation properties
        /// <summary>
        /// Gets/sets the post category.
        /// </summary>
        public Category Category { get; set; }
        #endregion
    }
}
