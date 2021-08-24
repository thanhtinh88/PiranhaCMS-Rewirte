using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace Piranha.Models
{
    public class BlockModel : BlockModel<BlockModel>
    {
        #region Properties
        public dynamic Regions { get; set; }
        #endregion

        public BlockModel(): base()
        {
            Regions = new ExpandoObject();
        }
    }

    /// <summary>
    /// Generic block model.
    /// </summary>
    /// <typeparam name="T">The model type</typeparam>
    public class BlockModel<T> : BlockModelBase where T: BlockModel<T>
    {
        /// <summary>
        /// Creates a new block model using the given block type id.
        /// </summary>
        /// <param name="typeId">The unique block type id</param>
        /// <returns>The new model</returns>
        public static T Create(string typeId)
        {
            using (var factory = new ContentFactory(App.BlockTypes))
            {
                return factory.Create<T>(typeId);
            }
        }

        /// <summary>
        /// Creates a new region.
        /// </summary>
        /// <param name="typeId">The block type id</param>
        /// <param name="regionId">The region id</param>
        /// <returns>The new region value</returns>
        public static object CreateRegion(string typeId, string regionId)
        {
            using (var factory = new ContentFactory(App.BlockTypes))
            {
                return factory.CreateRegion(typeId, regionId);
            }
        }
    }

    /// <summary>
    /// Base class for block models.
    /// </summary>
    public class BlockModelBase: Content
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
