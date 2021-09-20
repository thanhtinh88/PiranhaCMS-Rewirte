using Microsoft.EntityFrameworkCore;
using Piranha.EF.Data;
using Piranha.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Piranha.EF.Repositories
{
    /// <summary>
    /// The client post repository.
    /// </summary>
    public class PostRepository : RepositoryBase<Data.Post, Models.Post>, IPostRepository
    {
        internal PostRepository(Db db) : base(db) { }

        /// <summary>
		/// Gets the post model with the specified id.
		/// </summary>
		/// <param name="id">The unique id</param>
		/// <returns>The post model</returns>
        public T GetById<T>(Guid id) where T : Models.Post
        {
            var post = Query()
                .Where(p => p.Id == id && p.Published <= DateTime.Now)
                .FirstOrDefault();
            if (post != null)
                return Map<T>(post);
            return null;
        }

        /// <summary>
		/// Gets the post models that matches the given
		/// id array.
		/// </summary>
		/// <param name="id">The id array</param>
		/// <returns>The post models</returns>
        public IList<Models.Post> GetById(Guid[] id)
        {
            return GetById<Models.Post>(id);
        }

        /// <summary>
        /// Gets the post models that matches the given id array.
        /// </summary>
        /// <param name="id">The id array</param>
        /// <returns>The post models</returns>
        public IList<T> GetById<T>(Guid[] id) where T: Models.Post
        {
            var models = new List<T>();
            var result = Query().Where(p => id.Contains(p.Id)).ToList();

            foreach (var post in result)
            {
                models.Add(Map<T>(post));
            }

            return models;
        }


        public Models.Post GetBySlug(string category, string slug)
        {
            return GetBySlug<Models.Post>(category, slug);
        }

        /// <summary>
		/// Gets the post model with the specified slug.
		/// </summary
		/// <param name="category">The category id</param>
		/// <param name="slug">The unique slug</param>
		/// <returns>The post model</returns>
        public T GetBySlug<T>(string category, string slug) where T : Models.Post
        {
            var post = Query()
                .Where(p => p.Category.Slug == category && p.Slug == slug && p.Published <= DateTime.Now)
                .FirstOrDefault();
            if (post != null)
                return Map<T>(post);
            return null;
        }


        protected override IQueryable<Post> Query()
        {
            return db.Posts
                .Include(p => p.Category);
        }


        /// <summary>
        /// Transforms the given post into a full model.
        /// </summary>
        /// <param name="post">The post</param>
        /// <returns>The transformed model</returns>
        private T Map<T>(Post post) where T : Models.Post
        {
            var model = Activator.CreateInstance<T>();
            // Map basic fields
            Module.Mapper.Map<Data.Post, Models.Post>(post, model);

            // Map category
            model.Category = Module.Mapper.Map<Data.Category, Models.Category>(post.Category);
            return model;
        }
    }
}
