using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.EF.Data
{
    public class BlockType: ICreated, IModified
    {
        #region Properties
        /// <summary>
        /// Gets/sets the unique id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets/sets the JSON serialized body of the page type.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Gets/sets the created date.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Gets/sets the last modification date.
        /// </summary>
        public DateTime LastModified { get; set; }
        #endregion

        #region Navigation properties
        /// <summary>
        /// Gets/sets the pages of this type.
        /// </summary>
        public IList<Block> Blocks { get; set; }
        #endregion

        public BlockType()
        {
            Blocks = new List<Block>();
        }
    }
}
