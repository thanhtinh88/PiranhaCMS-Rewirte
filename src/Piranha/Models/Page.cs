using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.Models
{
    /// <summary>
    /// Generic page model.
    /// </summary>
    /// <typeparam name="T">The model type</typeparam>
    public class Page<T> : PageBase where T: Page<T>
    {
        #region Properties
        /// <summary>
        /// Gets if this is the startpage of the site.
        /// </summary>
        public bool IsStartPage
        {
            get => !ParentId.HasValue && SortOrder == 0;
        }
        #endregion

        /// <summary>
        /// Creates a new page model using the given page type id.
        /// </summary>
        /// <param name="api">The current api</param>
        /// <param name="typeId">The unique page type id</param>
        /// <returns>The new model</returns>
        public static T Create(Api api, string typeId = null)
        {
            if (string.IsNullOrWhiteSpace(typeId))
                typeId = typeof(T).Name;

            using (var factory = new ContentFactory(api.PageTypes.GetAll()))
            {
                return factory.Create<T>(typeId);
            }
        }

        /// <summary>
        /// Creates a new region.
        /// </summary>
        /// <param name="api">The current api</param>
        /// <param name="typeId">The page type id</param>
        /// <param name="regionId">The region id</param>
        /// <returns>The new region value</returns>
        public static object CreateRegion(Api api, string typeId, string regionId)
        {
            using (var factory = new ContentFactory(api.PageTypes.GetAll()))
            {
                return factory.CreateDynamicRegion(typeId, regionId);
            }
        }
    }
}
