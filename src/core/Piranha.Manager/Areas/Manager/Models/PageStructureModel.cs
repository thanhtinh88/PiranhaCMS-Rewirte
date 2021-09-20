using System;
using System.Collections.Generic;

namespace Piranha.Areas.Manager.Models
{
    public class PageStructureModel
    {
        public class PageStructureItem
        {
            /// <summary>
            /// Gets/sets the unique page id.
            /// </summary>
            public Guid Id { get; set; }

            /// <summary>
            /// Gets/sets the available children.
            /// </summary>
            public IList<PageStructureItem> Children { get; set; }


            public PageStructureItem()
            {
                Children = new List<PageStructureItem>();
            }
        }

        /// <summary>
        /// Gets/sets the structure items.
        /// </summary>
        public IList<PageStructureItem> Items { get; internal set; }

        public PageStructureModel()
        {
            Items = new List<PageStructureItem>();
        }
    }
}