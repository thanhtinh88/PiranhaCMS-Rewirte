using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.Data.Data
{
    /// <summary>
    /// Tags are used to classify content.
    /// </summary>
    public sealed class Tag: Base.Taxonomy, INotify
    {
        #region Notifications
        /// <summary>
        /// Called right before the model is about to be saved.
        /// </summary>
        /// <param name="db">The current db context</param>
        public void OnSave(Db db)
        {
            Hooks.Data.Tag.OnSave(db, this);
        }

        /// <summary>
        /// Executed right before the model is about to be deleted.
        /// </summary>
        /// <param name="db">The current db context</param>
        public void OnDelete(Db db)
        {
            Hooks.Data.Tag.OnDelete(db, this);
        }
        #endregion
    }
}
