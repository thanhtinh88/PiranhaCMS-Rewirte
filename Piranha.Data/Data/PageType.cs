using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.Data.Data
{
    /// <summary>
    /// Page fields hold the actual content of the pages. 
    /// </summary>
    public sealed class PageType: Base.ContentType<PageType, PageTypeField>, INotify
    {
        public PageType(): base()
        {

        }

        #region Notifications
        /// <summary>
        /// Called right before the model is about to be saved.
        /// </summary>
        /// <param name="db">The current db context</param>
        public void OnSave(Db db)
        {
            if (Hooks.Data.PageType.OnSave != null)
            {
                Hooks.Data.PageType.OnSave(db, this);
            }
            
        }

        /// <summary>
        /// Executed right before the model is about to be deleted.
        /// </summary>
        /// <param name="db">The current db context</param>
        public void OnDelete(Db db)
        {
            if (Hooks.Data.PageType.OnDelete != null)
            {
                Hooks.Data.PageType.OnDelete(db, this);
            }            
        }
        #endregion
    }
}
