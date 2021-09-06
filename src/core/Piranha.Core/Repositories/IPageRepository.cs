using Piranha.Models;
using System;
using System.Collections.Generic;

namespace Piranha.Repositories
{
    /// <summary>
	/// The client page repository interface.
	/// </summary>
    public interface IPageRepository
    {
		/// <summary>
		/// Gets the site startpage.
		/// </summary>
		/// <returns>The page model</returns>
		PageModel GetStartPage();

		/// <summary>
		/// Gets the site startpage.
		/// </summary>
		/// <typeparam name="T">The model type</typeparam>
		/// <returns>The page model</returns>
		T GetStartPage<T>() where T : Models.PageModel<T>;

		/// <summary>
		/// Gets the page model with the specified id.
		/// </summary>
		/// <param name="id">The unique id</param>
		/// <returns>The page model</returns>
		PageModel GetById(Guid id);

        /// <summary>
		/// Gets the page model with the specified id.
		/// </summary>
		/// <typeparam name="T">The model type</typeparam>
		/// <param name="id">The unique id</param>
		/// <returns>The page model</returns>
        T GetById<T>(Guid id) where T : Models.PageModel<T>;

		/// <summary>
		/// Gets the page model with the specified slug.
		/// </summary>
		/// <param name="slug">The unique slug</param>
		/// <returns>The page model</returns>
		PageModel GetBySlug(string slug);

		/// <summary>
		/// Gets the page model with the specified slug.
		/// </summary>
		/// <typeparam name="T">The model type</typeparam>
		/// <param name="slug">The unique slug</param>
		/// <returns>The page model</returns>
		T GetBySlug<T>(string slug) where T : Models.PageModel<T>;

		/// <summary>
		/// Gets all page models with the specified parent id.
		/// </summary>
		/// <param name="parentId">The parent id</param>
		/// <returns>The page models</returns>
		IList<PageModel> GetByParentId(Guid? parentId);

		/// <summary>
		/// Saves the given page model
		/// </summary>
		/// <param name="model">The page model</param>
		void Save<TModelType>(TModelType model) where TModelType : PageModel<TModelType>;
	}
}