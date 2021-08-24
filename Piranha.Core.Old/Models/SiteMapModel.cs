using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.Core.Models
{
    public class SiteMapModel
    {
        #region Properties
        /// <summary>
        /// Gets/sets the main title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets/sets the optional navigation title.
        /// </summary>
        public string NavigationTitle { get; set; }

        /// <summary>
        /// Gets/sets the unique slug.
        /// </summary>
        public string Slug { get; set; }

        /// <summary>
        /// Gets/sets the external permalink.
        /// </summary>
        public string Permalink { get; set; }

        /// <summary>
        /// Gets/sets the structural level.
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// Gets/sets the available children.
        /// </summary>
        public IList<SiteMapModel> Children { get; set; }
        #endregion

        public SiteMapModel()
        {
            Children = new List<SiteMapModel>();
        }
    }
}
