using Piranha.Extend;
using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.Repositories
{
    public interface IBlockTypeRepository
    {
        /// <summary>
        /// Gets the block type with the specified id.
        /// </summary>
        /// <param name="id">The unique id</param>
        /// <returns>The block type</returns>
        BlockType GetById(string id);

        /// <summary>
        /// Gets all available block types.
        /// </summary>
        /// <returns>The block types</returns>
        IList<BlockType> Get();

        /// <summary>
        /// Saves the given block type.
        /// </summary>
        /// <param name="blockType">The block type</param>
        void Save(BlockType blockType);

        /// <summary>
        /// Deletes the given block type.
        /// </summary>
        /// <param name="blockType">The block type</param>
        void Delete(BlockType blockType);

        /// <summary>
        /// Deletes the block type with the specified id.
        /// </summary>
        /// <param name="id">The unique id</param>
        void Delete(string id);
    }
}
