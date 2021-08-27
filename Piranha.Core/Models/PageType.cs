using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.Models
{ 
    /// <summary>
    /// Page fields hold the actual content of the pages. 
    /// </summary>
    public sealed class PageType
    {
        #region Properties
        public string Id { get; set; }
        public string Title { get; set; }
        public string Route { get; set; }
        public IList<PageTypeRegion> Regions { get; set; }
        #endregion

        internal PageType()
        {
            Regions = new List<PageTypeRegion>();
        }

        internal void Ensure()
        {
            
        }
    }
}
