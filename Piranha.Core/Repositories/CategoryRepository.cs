using Piranha.Data;
using Piranha.Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Piranha.Core.Repositories
{
    public class CategoryRepository
    {
        #region Members
        private readonly Db db;
        #endregion

        internal CategoryRepository(Db db)
        {
            this.db = db;
        }

        public Category GetById(Guid id)
        {
            return db.Categories
                .SingleOrDefault(c => c.Id == id);
        }

        public Category GetBySlug(string slug)
        {
            return db.Categories
                .SingleOrDefault(c => c.Slug == slug);
        }
    }
}
