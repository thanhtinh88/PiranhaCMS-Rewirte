﻿using Microsoft.EntityFrameworkCore;
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
    /// The client post repository.
    /// </summary>
    public class PostRepository
    {
        #region Member
        /// <summary>
        /// The current db context.
        /// </summary>
        private readonly Db db;
        #endregion

        internal PostRepository(Db db)
        {
            this.db = db;
        }

        /// <summary>
        /// Gets the post model with the specified id.
        /// </summary>
        /// <param name="id">The unique id</param>
        /// <returns>The post model</returns>
        public PostModel GetById(Guid id)
        {
            var post = FullQuery()
                .Where(p => p.Id == id && p.Published <= DateTime.Now)
                .FirstOrDefault();
            if (post != null)
                return FullTransform(post);
            return null;
        }

        /// <summary>
        /// Gets the post model with the specified slug.
        /// </summary>
        /// <param name="categoryId">The category id</param>
        /// <param name="slug">The unique slug</param>
        /// <returns>The post model</returns>
        public PostModel GetBySlug(Guid categoryId, string slug)
        {
            var post = FullQuery()
                .Where(p => p.CategoryId == categoryId && p.Slug == slug && p.Published <= DateTime.Now)
                .FirstOrDefault();
            if (post != null)
                return FullTransform(post);
            return null;
        }

        #region Private methods
        /// <summary>
        /// Gets the full post query.
        /// </summary>
        /// <returns>The queryable</returns>
        private IQueryable<Post> FullQuery()
        {
            return db.Posts
                .Include(p => p.Author)
                .Include(p => p.Category)
                .Include(p => p.Type.Fields)
                .Include(p => p.Fields);
        }

        /// <summary>
        /// Transforms the given post into a full model.
        /// </summary>
        /// <param name="post">The post</param>
        /// <returns>The transformed model</returns>
        private PostModel FullTransform(Post post)
        {
            // Map basic fields
            var model = App.Mapper.Map<Post, PostModel>(post);

            // Map additional fields
            model.Route = !string.IsNullOrEmpty(post.Route) ? post.Route :
                !string.IsNullOrEmpty(post.Type.Route) ? post.Type.Route : "/post";
            model.Permalink = $"~/{post.Category.Slug}/{post.Slug}";

            // Map regions
            foreach (var fieldType in post.Type.Fields.Where(f => f.FieldType == FieldType.Region))
            {
                var field = post.Fields.SingleOrDefault(f => f.TypeId == fieldType.Id);
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
