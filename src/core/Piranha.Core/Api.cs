using Piranha.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha
{
    public class Api: IApi
    {
        #region members
        /// <summary>
        /// The private data service.
        /// </summary>
        private readonly IDataService service;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the archive repository.
        /// </summary>
        public IArchiveRepostiory Archives { get => service.Archives; }

        /// <summary>
        /// Gets the block type repository.
        /// </summary>
        public IBlockTypeRepository BlockTypes { get => service.BlockTypes; }

        /// <summary>
        /// Gets the category repository.
        /// </summary>
        public ICategoryRepository Categories { get => service.Categories; }

        /// <summary>
        /// Gets the page repository.
        /// </summary>
        public IPageRepository Pages { get => service.Pages; }

        /// <summary>
        /// Gets the page type repository.
        /// </summary>
        public IPageTypeRepository PageTypes { get => service.PageTypes; }

        /// <summary>
        /// Gets the post repository.
        /// </summary>
        public IPostRepository Posts { get => service.Posts; }

        /// <summary>
        /// Gets the sitemap repository.
        /// </summary>
        public ISitemapRepository Sitemap { get => service.Sitemap; }
        #endregion

        public Api(IDataService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Disposes the Api.
        /// </summary>
        public void Dispose()
        {
            service.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
