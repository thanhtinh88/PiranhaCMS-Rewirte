using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.Builder.Json
{
    class PageTypeConfig
    {
        public IList<Extend.PageType> PageTypes { get; set; }

        public PageTypeConfig()
        {
            PageTypes = new List<Extend.PageType>();
        }
        internal void AssertConfigIsValid()
        {
            foreach (var pageType in PageTypes)
            {
                pageType.Ensure();
            }
        }
    }
}
