using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.Data.Data
{
    /// <summary>
    /// An uploaded media file
    /// </summary>
    public sealed class Media: IModel, ICreated, IModified, INotify
    {
        #region Properties
        /// <summary>
        /// Gets/sets the unique id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets/sets the optional folder id.
        /// </summary>
        public Guid? FolderId { get; set; }

        /// <summary>
        /// Gets/sets the filename.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Gets/sets the content type.
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// Gets/sets the size of the uploaded asset.
        /// </summary>
        public long Size { get; set; }

        /// <summary>
        /// Gets/sets the public url.
        /// </summary>
        public string PublicUrl { get; set; }

        /// <summary>
        /// Gets/sets the created date.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Gets/sets the last modified date.
        /// </summary>
        public DateTime LastModified { get; set; }
        #endregion

        #region Navigation Properties
        /// <summary>
        /// Gets/sets the optional folder.
        /// </summary>
        public MediaFolder Folder { get; set; }
        #endregion

        #region Notifications
        /// <summary>
        /// Called right before the model is about to be saved.
        /// </summary>
        /// <param name="db">The current db context</param>
        public void OnSave(Db db)
        {
            Hooks.Data.Media.OnSave(db, this);
        }

        /// <summary>
        /// Executed right before the model is about to be deleted.
        /// </summary>
        /// <param name="db">The current db context</param>
        public void OnDelete(Db db)
        {
            Hooks.Data.Media.OnDelete(db, this);
        }
        #endregion
    }
}
