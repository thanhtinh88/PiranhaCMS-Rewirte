using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.Data.Data
{
    /// <summary>
    /// Posts are used to create content without a fixed position in the sitemap.
    /// </summary>
    public sealed class Post: Base.Content<Post, PostField, PostType, PostTypeField>, IMeta, INotify
    {
        public Post(): base()
        {

        }

        #region Properties
        /// <summary>
        /// Gets/sets the category id.
        /// </summary>
        public Guid CategoryId { get; set; }

        /// <summary>
        /// Gets/sets the optional meta title.
        /// </summary>
        public string MetaTitle { get; set; }

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
        #endregion

        #region Navigation Properties
        /// <summary>
        /// Gets/sets the category.
        /// </summary>
        public Category Category { get; set; }
        #endregion

        #region Notifications
        /// <summary>
        /// Called right before the model is about to be saved.
        /// </summary>
        /// <param name="db">The current db context</param>
        public void OnSave(Db db)
        {
            if (Hooks.Data.Post.OnSave != null)
            {
                Hooks.Data.Post.OnSave(db, this); 
            }
        }

        /// <summary>
        /// Executed right before the model is about to be deleted.
        /// </summary>
        /// <param name="db">The current db context</param>
        public void OnDelete(Db db)
        {
            if (Hooks.Data.Post.OnSave != null)
            {
                Hooks.Data.Post.OnDelete(db, this); 
            }
        }
        #endregion
    }
}
