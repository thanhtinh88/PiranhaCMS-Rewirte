using Piranha.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Piranha.Core
{
    public interface IApi: IDisposable
    {
        /// <summary>
		/// Gets the archive repository.
		/// </summary>
        IArchiveRepostiory Archives { get; }

        /// <summary>
		/// Gets the category repository.
		/// </summary>
        ICategoryRepository Categories { get; }

        /// <summary>
		/// Gets the page repository.
		/// </summary>
        IPageRepository Pages { get; }

        /// <summary>
		/// Gets the post repository.
		/// </summary>
        IPostRepository Posts { get; }

        /// <summary>
		/// Gets the site map repository.
		/// </summary>
        ISiteMapRepository SiteMap { get; }

        /// <summary>
		/// Saves the changes made to the api.
		/// </summary>
		/// <returns>The number of saved rows.</returns>
        int SaveChanges();

        /// <summary>
		/// Saves the changes made to the api.
		/// </summary>
		/// <returns>The number of saved rows.</returns>
        Task<int> SaveChangeAsync();
    }
}
