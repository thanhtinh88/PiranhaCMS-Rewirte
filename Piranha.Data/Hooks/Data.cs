using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.Data.Hooks
{
    public static class Data
    {
        public static class Delegates
        {
            public delegate void OnNotifyDelegate<T>(Db db, T model);
        }

        /// <summary>
        /// The hooks available for the author model
        /// </summary>
        public static class Author
        {
            /// <summary>
            /// Called right before the author is saved.
            /// </summary>
            public static Delegates.OnNotifyDelegate<Piranha.Data.Data.Author> OnSave;

            /// <summary>
            /// Called right before the author is deleted.
            /// </summary>
            public static Delegates.OnNotifyDelegate<Piranha.Data.Data.Author> OnDelete;
        }

        /// <summary>
        /// The hooks available for the category model.
        /// </summary>
        public static class Category
        {
            /// <summary>
            /// Called right before the category is saved.
            /// </summary>
            public static Delegates.OnNotifyDelegate<Piranha.Data.Data.Category> OnSave;

            /// <summary>
            /// Called right before the category is deleted.
            /// </summary>
            public static Delegates.OnNotifyDelegate<Piranha.Data.Data.Category> OnDelete;
        }

        /// <summary>
        /// The hooks available for the Media model
        /// </summary>
        public static class Media
        {
            /// <summary>
            /// Called right before the media is saved.
            /// </summary>
            public static Delegates.OnNotifyDelegate<Piranha.Data.Data.Media> OnSave;

            /// <summary>
            /// Called right before the media is deleted.
            /// </summary>
            public static Delegates.OnNotifyDelegate<Piranha.Data.Data.Media> OnDelete;
        }

        /// <summary>
        /// The hooks available for the media folder model
        /// </summary>
        public static class MediaFolder
        {
            /// <summary>
            /// Called right before the media folder is saved.
            /// </summary>
            public static Delegates.OnNotifyDelegate<Piranha.Data.Data.MediaFolder> OnSave;

            /// <summary>
            /// Called right before the media folder is deleted.
            /// </summary>
            public static Delegates.OnNotifyDelegate<Piranha.Data.Data.MediaFolder> OnDelete;
        }

        /// <summary>
        /// The hooks available for the page model
        /// </summary>
        public static class Page
        {
            /// <summary>
            /// Called right before the page is saved.
            /// </summary>
            public static Delegates.OnNotifyDelegate<Piranha.Data.Data.Page> OnSave;

            /// <summary>
            /// Called right before the page is deleted.
            /// </summary>
            public static Delegates.OnNotifyDelegate<Piranha.Data.Data.Page> OnDelete;
        }

        /// <summary>
        /// The hooks available for the page type model
        /// </summary>
        public static class PageType
        {
            /// <summary>
            /// Called right before the page type is saved.
            /// </summary>
            public static Delegates.OnNotifyDelegate<Piranha.Data.Data.PageType> OnSave;

            /// <summary>
            /// Called right before the page type is deleted.
            /// </summary>
            public static Delegates.OnNotifyDelegate<Piranha.Data.Data.PageType> OnDelete;
        }

        /// <summary>
        /// The hooks available for the param model
        /// </summary>
        public static class Param
        {
            /// <summary>
            /// Called right before the param is saved.
            /// </summary>
            public static Delegates.OnNotifyDelegate<Piranha.Data.Data.Param> OnSave;

            /// <summary>
            /// Called right before the param is deleted.
            /// </summary>
            public static Delegates.OnNotifyDelegate<Piranha.Data.Data.Param> OnDelete;
        }

        /// <summary>
        /// The hooks available for the author model
        /// </summary>
        public static class Post
        {
            /// <summary>
            /// Called right before the post is saved.
            /// </summary>
            public static Delegates.OnNotifyDelegate<Piranha.Data.Data.Post> OnSave;

            /// <summary>
            /// Called right before the post is deleted.
            /// </summary>
            public static Delegates.OnNotifyDelegate<Piranha.Data.Data.Post> OnDelete;
        }

        /// <summary>
        /// The hooks available for the post type model
        /// </summary>
        public static class PostType
        {
            /// <summary>
            /// Called right before the post type is saved.
            /// </summary>
            public static Delegates.OnNotifyDelegate<Piranha.Data.Data.PostType> OnSave;

            /// <summary>
            /// Called right before the post type is deleted.
            /// </summary>
            public static Delegates.OnNotifyDelegate<Piranha.Data.Data.PostType> OnDelete;
        }

        /// <summary>
        /// The hooks available for the tag model
        /// </summary>
        public static class Tag
        {
            /// <summary>
            /// Called right before the tag is saved.
            /// </summary>
            public static Delegates.OnNotifyDelegate<Piranha.Data.Data.Tag> OnSave;

            /// <summary>
            /// Called right before the tag is deleted.
            /// </summary>
            public static Delegates.OnNotifyDelegate<Piranha.Data.Data.Tag> OnDelete;
        }
    }
}
