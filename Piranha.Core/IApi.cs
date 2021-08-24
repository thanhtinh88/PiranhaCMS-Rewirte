using Piranha.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Piranha
{
    public interface IApi: IDisposable
    {
        /// <summary>
		/// Gets the archive repository.
		/// </summary>
        IArchiveRepostiory Archives { get; }

        /// <summary>
        /// Gets the block type repository.
        /// </summary>
        IBlockTypeRepository BlockTypes { get; }

        /// <summary>
		/// Gets the category repository.
		/// </summary>
        ICategoryRepository Categories { get; }

        /// <summary>
		/// Gets the page repository.
		/// </summary>
        IPageRepository Pages { get; }

        /// <summary>
        /// Gets the page type repository.
        /// </summary>
        IPageTypeRepository PageTypes { get; }

        /// <summary>
		/// Gets the post repository.
		/// </summary>
        IPostRepository Posts { get; }

    }
}
