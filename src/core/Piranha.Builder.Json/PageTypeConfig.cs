using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.Builder.Json
{
    /// <summary>
    /// Config file for importing page types with json.
    /// </summary>
    class PageTypeConfig
    {
        /// <summary>
        /// The available page types.
        /// </summary>
        public IList<Extend.PageType> PageTypes { get; set; }

        public PageTypeConfig()
        {
            PageTypes = new List<Extend.PageType>();
        }

        /// <summary>
        /// Asserts that the page types are valid.
        /// </summary>
        public void AssertConfigIsValid()
        {
            foreach (var pageType in PageTypes)
            {
                pageType.Ensure();
            }
        }
    }
}
