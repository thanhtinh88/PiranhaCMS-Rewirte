using Microsoft.EntityFrameworkCore;
using Piranha.Data;
using Piranha.Data.Data;
using Piranha.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Piranha.Core.Repositories
{
    /// <summary>
    /// The client page repository
    /// </summary>
    public class PageRepository
    {
        #region Members
        /// <summary>
        /// The current db context.
        /// </summary>
        private readonly Db db;
        #endregion

        internal PageRepository(Db db)
        {
            this.db = db;
        }

        /// <summary>
        /// Gets the site startpage.
        /// </summary>
        /// <returns></returns>
        public PageModel GetStartPage()
        {
            return GetStartPage<PageModel>();
        }

        /// <summary>
        /// Gets the site startpage.
        /// </summary>
        /// <typeparam name="T">The model type</typeparam>
        /// <returns>The page model</returns>
        public T GetStartPage<T>() where T: PageModel
        {
            var page = FullQuery()
               .Where(p => p.ParentId == null && p.SortOrder == 0 && p.Published <= DateTime.Now)
               .SingleOrDefault();

            if (page != null)
                return FullTransform<T>(page);
            return null;
        }

        /// <summary>
        /// Gets the page model with the specified id.
        /// </summary>
        /// <param name="id">The unique id</param>
        /// <returns>The page model</returns>
        public PageModel GetById(Guid id)
        {
            return GetById<PageModel>(id);
        }

        /// <summary>
        /// Gets the page model with the specified id.
        /// </summary>
        /// <typeparam name="T">The model type</typeparam>
        /// <param name="id">The unique id</param>
        /// <returns>The page model</returns>
        public T GetById<T>(Guid id) where T: PageModel
        {
            var page = FullQuery()
                .Where(p => p.Id == id && p.Published <= DateTime.Now)
                .SingleOrDefault();

            if (page != null)
                return FullTransform<T>(page);
            return null;
        }

        /// <summary>
        /// Gets the page model with the specified slug.
        /// </summary>
        /// <param name="slug">The unique slug</param>
        /// <returns>The page model</returns>
        public PageModel GetBySlug(string slug)
        {
            return GetBySlug<PageModel>(slug);
        }

        /// <summary>
        /// Gets the page model with the specified slug.
        /// </summary>
        /// <typeparam name="T">The model type</typeparam>
        /// <param name="slug">The unique slug</param>
        /// <returns>The page model</returns>
        public T GetBySlug<T>(string slug) where T: PageModel
        { 
            var page = FullQuery()
                .Where(p => p.Slug == slug && p.Published <= DateTime.Now)
                .SingleOrDefault();

            if (page != null)
                return FullTransform<T>(page);
            return null;
        }

        /// <summary>
        /// Gets all page models with the specified parent id.
        /// </summary>
        /// <param name="parentId">The parent id</param>
        /// <returns>The page models</returns>
        public IList<PageModel> GetByParentId(Guid? parentId)
        {
            var pages = FullQuery()
                .Where(p => p.ParentId == parentId && p.Published <= DateTime.Now)
                .OrderBy(p => p.SortOrder)
                .ToList();

            var ret = new List<PageModel>();

            foreach (var page in pages)
            {
                ret.Add(FullTransform<PageModel>(page));
            }

            return ret;
        }


        #region Private methods
        /// <summary>
        /// Gets the full page query.
        /// </summary>
        /// <returns>The queryable</returns>
        private IQueryable<Page> FullQuery()
        {
            return db.Pages
                .Include(p => p.Author)
                .Include(p => p.Type.Fields)
                .Include(p => p.Fields);
        }

        /// <summary>
        /// Transforms the given page into a full page model.
        /// </summary>
        /// <param name="page">The page</param>
        /// <returns>The transformed model</returns>
        private T FullTransform<T>(Page page) where T:PageModel
        {
            var model = Activator.CreateInstance<T>();
            App.Mapper.Map<Page, PageModel>(page, model);

            model.Route = !string.IsNullOrEmpty(page.Route) ? page.Route : 
                !string.IsNullOrEmpty(page.Type.Route) ? page.Type.Route : "/page";

            model.Permalink = $"~/{page.Slug}";
            model.IsStartPage = !page.ParentId.HasValue && page.SortOrder == 0;

            // Map regions
            foreach (var fieldType in page.Type.Fields.Where(f => f.FieldType == FieldType.Region))
            {
                var field = page.Fields.SingleOrDefault(f => f.TypeId == fieldType.Id);
                if (field != null)
                {
                    var region = App.ExtensionManager.Deserialize(fieldType.CLRType, field.Value);
                    if (region != null)
                        ((IDictionary<string, object>)model.Regions)[fieldType.InternalId] = region.GetValue();
                }
            }
            return model;
        }
        #endregion
    }
}
