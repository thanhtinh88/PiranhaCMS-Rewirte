using Piranha.Data;
using Piranha.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Piranha.EF
{
    public class Api : IApi
    {
        #region Members
        /// <summary>
        /// The private db context.
        /// </summary>
        private readonly Db db;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the archive repository.
        /// </summary>
        public IArchiveRepostiory Archives { get; }

        /// <summary>
        /// Gets the block type repository.
        /// </summary>
        public IBlockTypeRepository BlockTypes { get; }

        /// <summary>
        /// Gets the category repository.
        /// </summary>
        public ICategoryRepository Categories { get; }

        /// <summary>
        /// Gets the page repository.
        /// </summary>
        public IPageRepository Pages { get; }

        /// <summary>
        /// Gets the page type repository.
        /// </summary>
        public IPageTypeRepository PageTypes { get; }

        /// <summary>
        /// Gets the post repository.
        /// </summary>
        public IPostRepository Posts { get;  }
        #endregion

        public Api(Db db)
        {
            this.db = db;
            Archives = new Repositories.ArchiveRepostiory(db);
            BlockTypes = new Repositories.BlockTypeRepository(db);
            Categories = new Repositories.CategoryRepository(db);
            Pages = new Repositories.PageRepository(db);
            PageTypes = new Repositories.PageTypeRepository(db);
            Posts = new Repositories.PostRepository(db);            
        }

        public void Dispose()
        {
            db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
