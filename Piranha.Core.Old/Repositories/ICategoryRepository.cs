using Piranha.Data.Data;
using System;

namespace Piranha.Core.Repositories
{
    /// <summary>
	/// The client category repository interface.
	/// </summary>
    public interface ICategoryRepository
    {
        /// <summary>
		/// Gets the category identified by the given id.
		/// </summary>
		/// <param name="id">The unique id</param>
		/// <returns>The category</returns>
        Category GetById(Guid id);

		/// <summary>
		/// Gets the category identified by the given slug.
		/// </summary>
		/// <param name="id">The unique slug</param>
		/// <returns>The category</returns>
		Category GetBySlug(string slug);
    }
}