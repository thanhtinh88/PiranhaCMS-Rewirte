using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace Piranha.Models
{
    public class DynamicBlock: Block<DynamicBlock>
    {
        #region Properties
        /// <summary>
        /// Gets/sets the regions.
        /// </summary>
        public dynamic Regions { get; set; }
        #endregion

        public DynamicBlock(): base()
        {
            Regions = new ExpandoObject();
        }
    }
}
