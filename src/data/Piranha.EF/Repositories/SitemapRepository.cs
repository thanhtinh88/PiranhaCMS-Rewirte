using Piranha.EF.Data;
using Piranha.Models;
using Piranha.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Piranha.EF.Repositories
{
    public class SitemapRepository: ISitemapRepository
    {
        #region Members
        /// <summary>
        /// The current db context.
        /// </summary>
        private readonly Db db;
        #endregion

        internal SitemapRepository(Db db)
        {
            this.db = db;
        }

        /// <summary>
        /// Gets the hierachical sitemap structure.
        /// </summary>
        /// <param name="onlyPublished">If only published items should be included</param>
        /// <returns>The sitemap</returns>
        public IList<Models.SitemapItem> Get(bool onlyPublished = true)
        {
            IQueryable<Data.Page> query = db.Pages;
            if (onlyPublished)
                query = query.Where(p => p.Published <= DateTime.Now);

            var pages = query
                .OrderBy(p => p.ParentId)
                .ThenBy(p => p.SortOrder)
                .ToList();

            return Sort(pages);
        }

        #region Private methods
        /// <summary>
        /// Sorts the items.
        /// </summary>
        /// <param name="pages">The full page list</param>
        /// <param name="parentId">The current parent id</param>
        /// <returns>The sitemap</returns>
        private IList<SitemapItem> Sort(List<Page> pages, Guid? parentId = null, int level = 0)
        {
            var result = new List<Models.SitemapItem>();

            foreach (var page in pages.Where(p => p.ParentId == parentId).OrderBy(p => p.SortOrder))
            {
                var item = new Models.SitemapItem()
                {
                    Id = page.Id,
                    Title = page.Title,
                    NavigationTitle = page.NavigationTitle,
                    Permalink = page.Slug,
                    Level = level,
                    Created = page.Created,
                    LastModified = page.LastModified
                };
                item.Items = Sort(pages, page.Id, level + 1);
                result.Add(item);
            }
            return result;
        } 
        #endregion
    }
}
