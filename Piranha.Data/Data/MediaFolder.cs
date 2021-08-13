using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.Data.Data
{
    /// <summary>
    /// Hierarchical folders for the media library.
    /// </summary>
    public sealed class MediaFolder : IModel, INotify
    {
        #region Properties
        /// <summary>
        /// Gets/sets the unique id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets/sets the optional parent id.
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// Gets/sets the display name.
        /// </summary>
        public string Name { get; set; }
        #endregion

        #region Notifications
        /// <summary>
        /// Called right before the model is about to be saved.
        /// </summary>
        /// <param name="db">The current db context</param>
        public void OnSave(Db db)
        {
            if (Hooks.Data.MediaFolder.OnSave != null)
            {
                Hooks.Data.MediaFolder.OnSave(db, this); 
            }
        }

        /// <summary>
        /// Executed right before the model is about to be deleted.
        /// </summary>
        /// <param name="db">The current db context</param>
        public void OnDelete(Db db)
        {
            if (Hooks.Data.MediaFolder.OnDelete != null)
            {
                Hooks.Data.MediaFolder.OnDelete(db, this); 
            }
        }
        #endregion
    }
}
