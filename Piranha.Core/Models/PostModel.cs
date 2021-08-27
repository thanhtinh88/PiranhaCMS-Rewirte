using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace Piranha.Models
{
    /// <summary>
    /// The client post model.
    /// </summary>
    public class PostModel : PostBase
    {
        #region Properties
        /// <summary>
        /// Gets/sets the public permalink.
        /// </summary>
        public string Permalink { get; set; }

        /// <summary>
        /// Gets/sets the category.
        /// </summary>
        public Category Category { get; set; }

        /// <summary>
        /// Gets/sets the available regions.
        /// </summary>
        public IList<Tag> Tags { get; set; }
        #endregion
        public PostModel()
        {
            Tags = new List<Tag>();
        }
    }
}
