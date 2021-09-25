using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace Piranha.Models
{
    /// <summary>
    /// Dynamic page model.
    /// </summary>
    public class DynamicPage: Page<DynamicPage>
    {
        #region Properties
        /// <summary>
        /// Gets/sets the regions.
        /// </summary>
        public dynamic Regions { get; set; }
        #endregion

        public DynamicPage()
        {
            Regions = new ExpandoObject();
        }
    }
}
