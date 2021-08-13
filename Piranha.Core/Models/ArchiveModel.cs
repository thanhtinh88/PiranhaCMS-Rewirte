using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.Core.Models
{
    /// <summary>
    /// The client archive model.
    /// </summary>
    public class ArchiveModel
    {
        #region Properties
        /// <summary>
        /// Gets/sets the unique id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets/sets the archive title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets/sets the archive slug.
        /// </summary>
        public string Slug { get; set; }

        /// <summary>
        /// Gets/sets the optional description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets/sets the optional route.
        /// </summary>
        public string Route { get; set; }

        /// <summary>
        /// Gets/sets the currently requested year.
        /// </summary>
        public int? Year { get; set; }

        /// <summary>
        /// Gets/sets the currently requested month.
        /// </summary>
        public int? Month { get; set; }

        /// <summary>
        /// Gets/sets the currently requested page.
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Gets/sets the total page count.
        /// </summary>
        public int PageCount { get; set; }

        /// <summary>
        /// Gets/sets the available posts.
        /// </summary>
        public IList<PostListModel> Posts { get; set; }
        #endregion

        public ArchiveModel()
        {
            Posts = new List<PostListModel>();
        }
    }
}
