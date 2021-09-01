using Piranha.Extend;
using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.Repositories
{
    public interface IPageTypeRepository
    {
        /// <summary>
        /// Gets the page type with the specified id.
        /// </summary>
        /// <param name="id">The unique id</param>
        /// <returns>The page type</returns>
        PageType GetById(string id);

        /// <summary>
        /// Gets all available page types.
        /// </summary>
        /// <returns>The page types</returns>
        IList<PageType> Get();

        /// <summary>
        /// Saves the given page type.
        /// </summary>
        /// <param name="pageType">The page type</param>
        void Save(PageType pageType);

        /// <summary>
        /// Deletes the given page type.
        /// </summary>
        /// <param name="pageType">The page type</param>
        void Delete(PageType pageType);

        /// <summary>
        /// Deletes the page type with the specified id.
        /// </summary>
        /// <param name="id">The unique id</param>
        void Delete(string id);
    }
}
