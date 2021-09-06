using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.EF.Data
{
    /// <summary>
    /// Interface for models that should be notified before changes are saved.
    /// </summary>
    public interface INotify
    {
        /// <summary>
        /// Called right before the model is about to be saved.
        /// </summary>
        /// <param name="db">The current db context</param>
        void OnSave(Db db);

        /// <summary>
        /// Executed right before the model is about to be deleted.
        /// </summary>
        /// <param name="db">The current db context</param>
        void OnDelete(Db db);
    }
}
