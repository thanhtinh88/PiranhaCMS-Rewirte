using Piranha.Data;
using Piranha.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Piranha.Core
{
    public class Api : IDisposable
    {
        #region Members
        private readonly Db db;
        #endregion

        #region Properties
        public CategoryRepository Categories { get; set; }
        public PageRepository Pages { get; set; }
        public PostRepository Posts { get; set; }
        #endregion

        public Api()
        {
            db = new Db();
            Categories = new CategoryRepository(db);
            Pages = new PageRepository(db);
            Posts = new PostRepository(db);
        }

        public int SaveChanges()
        {
            return db.SaveChanges();
        }

        public Task<int> SaveChangeAsync()
        {
            return db.SaveChangesAsync();
        }
        public void Dispose()
        {
            db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
