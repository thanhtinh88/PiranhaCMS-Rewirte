using Piranha.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Piranha.EF.Repositories
{
    /// <summary>
    /// The client category repository.
    /// </summary>
    public class CategoryRepository : RepositoryBase<Data.Category, Models.Category>, ICategoryRepository
    {
        internal CategoryRepository(Db db) : base(db) { }

        /// <summary>
		/// Gets the category with the specified slug.
		/// </summary>
		/// <param name="slug">The unique slug</param>
		/// <returns>The category</returns>
        public Models.Category GetBySlug(string slug)
        {
            var result = Query().SingleOrDefault(c => c.Slug == slug);
                        
            if (result != null)
                return Map(result);
            return null;
        }

        /// <summary>
		/// Gets the full category model with the specified id.
		/// </summary>
		/// <param name="id">The unique id</param>
		/// <returns>The category</returns>
        public Models.CategoryModel GetModelById(Guid id)
        {
            var result = Query().SingleOrDefault(c => c.Id == id);

            if (result != null)
                return MapModel(result);
            return null;
        }

        /// <summary>
		/// Gets the full category model with the specified slug.
		/// </summary>
		/// <param name="slug">The unique slug</param>
		/// <returns>The category</returns>
        public Models.CategoryModel GetModelBySlug(string slug)
        {
            var result = Query().SingleOrDefault(c => c.Slug == slug);

            if (result != null)
                return MapModel(result);
            return null;
        }

        /// <summary>
		/// Saves the category.
		/// </summary>
		/// <param name="model">The category</param>
        public void Save(Models.Category model)
        {
            var category = db.Categories.SingleOrDefault(c => c.Id == model.Id);
            if (category == null)
            {
                category = new Data.Category()
                {
                    Id = Guid.NewGuid()
                };

                db.Categories.Add(category);
                model.Id = category.Id;
            }
            Module.Mapper.Map<Models.Category, Data.Category>(model, category);
            db.SaveChanges();
        }

        /// <summary>
		/// Saves the full category model.
		/// </summary>
		/// <param name="model">The full model</param>
        public void Save(Models.CategoryModel model)
        {
            var category = db.Categories.SingleOrDefault(c => c.Id == model.Id);
            if (category == null)
            {
                category = new Data.Category()
                {
                    Id = Guid.NewGuid()
                };
                db.Categories.Add(category);
                model.Id = category.Id;
            }

            Module.Mapper.Map<Models.CategoryModel, Data.Category>(model, category);
            db.SaveChanges();
        }

        /// <summary>
		/// Maps the given result to the full category model.
		/// </summary>
		/// <param name="result">The result</param>
		/// <returns>The model</returns>
        public Models.CategoryModel MapModel(Data.Category result)
        {
            return Module.Mapper.Map<Data.Category, Models.CategoryModel>(result);
        }
    }
}
