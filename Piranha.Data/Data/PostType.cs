namespace Piranha.Data.Data
{
    /// <summary>
    /// Post types are used to define reusable post templates.
    /// </summary>
    public sealed class PostType: Base.ContentType<PostType, PostTypeField>, INotify
    {
        public PostType(): base()
        {

        }

        #region Notifications
        /// <summary>
        /// Called right before the model is about to be saved.
        /// </summary>
        /// <param name="db">The current db context</param>
        public void OnSave(Db db)
        {
            if (Hooks.Data.PostType.OnSave != null)
            {
                Hooks.Data.PostType.OnSave(db, this); 
            }
        }

        /// <summary>
        /// Executed right before the model is about to be deleted.
        /// </summary>
        /// <param name="db">The current db context</param>
        public void OnDelete(Db db)
        {
            if (Hooks.Data.PostType.OnDelete != null)
            {
                Hooks.Data.PostType.OnDelete(db, this); 
            }
        }
        #endregion
    }
}