using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace Piranha.Models
{
    public class BlockModel: BlockModel<BlockModel>
    {
        #region Properties
        /// <summary>
        /// Gets/sets the regions.
        /// </summary>
        public dynamic Regions { get; set; }
        #endregion

        public BlockModel() : base()
        {
            Regions = new ExpandoObject();
        }
    }

    public class BlockModel<T>: BlockModelBase where T: BlockModel<T>
    {
        public static T Create(string typeId)
        {
            using (var factory = new ContentFactory(App.BlockTypes))
            {
                return factory.Create<T>(typeId);
            }
        }

        public static object CreateRegion(string typeId, string regionId)
        {
            using (var factory = new ContentFactory(App.BlockTypes))
            {
                return factory.CreateRegion(typeId, regionId);
            }
        }
    }

    public abstract class BlockModelBase: Content
    {
        #region Properties
        /// <summary>
        /// Gets/sets the optional view that should be used
        /// to render this block.
        /// </summary>
        public string View { get; set; }
        #endregion
    }
}
