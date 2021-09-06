using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.Builder.Json
{
    /// <summary>
    /// Config file for importing block types with json.
    /// </summary>
    internal class BlockTypeConfig
    {
        /// <summary>
        /// The available block types.
        /// </summary>
        public IList<Extend.BlockType> BlockTypes { get; internal set; }

        public BlockTypeConfig()
        {
            BlockTypes = new List<Extend.BlockType>();
        }

        /// <summary>
        /// Asserts that the block types are valid.
        /// </summary>
        internal void AssertConfigIsValid()
        {
            foreach (var blockType in BlockTypes)
            {
                blockType.Ensure();
            }
        }
    }
}
