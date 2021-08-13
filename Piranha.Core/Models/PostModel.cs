using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace Piranha.Core.Models
{
    /// <summary>
    /// The client post model.
    /// </summary>
    public class PostModel : PostListModel
    {
        public PostModel()
        {
            Regions = new ExpandoObject();
        }

        #region Properties
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
        /// Gets/sets the available regions.
        /// </summary>
        public dynamic Regions { get; set; }

        /// <summary>
        /// Gets/sets the internal route used by the middleware.
        /// </summary>
        internal string Route { get; set; }
        #endregion
    }
}
