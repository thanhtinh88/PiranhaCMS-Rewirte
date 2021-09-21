using Piranha.Models;
using System;
using System.Collections.Generic;

namespace Piranha.Repositories
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
        CategoryItem GetById(Guid id);

		/// <summary>
		/// Gets the category identified by the given slug.
		/// </summary>
		/// <param name="id">The unique slug</param>
		/// <returns>The category</returns>
		CategoryItem GetBySlug(string slug);

		/// <summary>
		/// Gets the full category model with the specified id.
		/// </summary>
		/// <param name="id">The unique id</param>
		/// <returns>The category</returns>
		Category GetModelById(Guid id);

		/// <summary>
		/// Gets the full category model with the specified slug.
		/// </summary>
		/// <param name="slug">The unique slug</param>
		/// <returns>The category</returns>
		Category GetModelBySlug(string slug);

		/// <summary>
		/// Gets the available categories.
		/// </summary>
		/// <param name="id">The unique id</param>
		/// <returns>The category</returns>
		IList<CategoryItem> Get(Guid id);

		/// <summary>
		/// Saves the category.
		/// </summary>
		/// <param name="model">The category</param>
		void Save(CategoryItem category);

		/// <summary>
		/// Saves the full category model.
		/// </summary>
		/// <param name="model">The full model</param>
		void Save(Category category);
    }
}