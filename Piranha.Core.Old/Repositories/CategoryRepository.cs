using Piranha.Data;
using Piranha.Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Piranha.Core.Repositories
{
    /// <summary>
    /// The client category repository.
    /// </summary>
    public class CategoryRepository : ICategoryRepository
    {
        #region Members
        private readonly Db db;
        #endregion

        internal CategoryRepository(Db db)
        {
            this.db = db;
        }

        /// <summary>
        /// Gets the category identified by the given id.
        /// </summary>
        /// <param name="id">The unique id</param>
        /// <returns>The category</returns>
        public Category GetById(Guid id)
        {
            return db.Categories
                .SingleOrDefault(c => c.Id == id);
        }

        /// <summary>
        /// Gets the category identified by the given slug.
        /// </summary>
        /// <param name="slug">The unique slug</param>
        /// <returns>The category</returns>
        public Category GetBySlug(string slug)
        {
            return db.Categories
                .SingleOrDefault(c => c.Slug == slug);
        }
    }
}
