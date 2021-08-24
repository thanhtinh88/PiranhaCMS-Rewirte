﻿using Microsoft.EntityFrameworkCore;
using Piranha.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Piranha.EF.Repositories
{
    /// <summary>
    /// The client archive repository.
    /// </summary>
    public class ArchiveRepostiory : IArchiveRepostiory
    {
        #region Members
        /// <summary>
        /// The current db context.
        /// </summary>
        private readonly Db db;

        /// <summary>
        /// TODO: This should be configurable.
        /// </summary>
        private const int ArchivePageSize = 5;
        #endregion

        internal ArchiveRepostiory(Db db)
        {
            this.db = db;
        }

        /// <summary>
        /// Gets the archive model for the specified category & period.
        /// </summary>
        /// <param name="id">The unique category id</param>
        /// <param name="page">The optional archive page</param>
        /// <param name="year">The optional year</param>
        /// <param name="month">The optional month</param>
        /// <returns></returns>
        public Models.ArchiveModel GetById(Guid id, int? page = null, int? year = null, int? month = null)
        {
            return GetById<Models.ArchiveModel>(id, page, year, month);
        }

        /// <summary>
        /// Gets the post archive for the category with the given id.
        /// </summary>
        /// <param name="id">The unique category id</param>
        /// <param name="page">The optional page</param>
        /// <param name="year">The optional year</param>
        /// <param name="month">The optional month</param>
        /// <returns>The archive model</returns>
        public T GetById<T>(Guid id, int? page = null, int? year = null, int? month = null) where T: Models.ArchiveModel
        { 
            // Get the requested category
            var category = db.Categories
                .SingleOrDefault(c => c.Id == id);
            if (category != null)
            {
                // Set basic fields
                var model = Activator.CreateInstance<T>();
                model.Id = id;
                model.Title = category.Title;
                model.Slug = category.Slug;
                model.MetaDescription = category.ArchiveDescription ?? category.Description;
                model.Route = !string.IsNullOrEmpty(category.ArchiveRoute) ? category.ArchiveRoute : "/archive";
                model.Year = year;
                model.Month = month;
                model.Page = Math.Max(1, page.HasValue ? page.Value : 1);

                // Built the query.
                var now = DateTime.Now;
                var query = db.Posts
                    //.Include(p => p.Author)
                    .Include(p => p.Category)
                    .Where(p => p.CategoryId == id && p.Published <= now);

                if (year.HasValue)
                {
                    DateTime from;
                    DateTime to;

                    if (month.HasValue)
                    {
                        from = new DateTime(year.Value, month.Value, 1);
                        to = from.AddMonths(1);
                    }
                    else
                    {
                        from = new DateTime(year.Value, 1, 1);
                        to = from.AddYears(1);
                    }

                    query = query.Where(p => p.Published >= from && p.Published < to);
                }

                // Get the total page count for the archive
                model.TotalPages = Math.Max(Convert.ToInt32(Math.Ceiling((double)query.Count() / ArchivePageSize)), 1);
                model.Page = Math.Min(model.Page, model.TotalPages);

                // Get the posts
                var posts = query
                    .OrderBy(p => p.Published)
                    .Skip((model.Page - 1)*ArchivePageSize)
                    .Take(ArchivePageSize)
                    .ToList();

                // Map & add the posts within the requested page
                var mapper = Module.Mapper;
                foreach (var post in posts)
                {
                    // Map fields
                    var postModel = mapper.Map<Data.Post, Models.PostModel>(post);
                    postModel.Category = mapper.Map<Data.Category, Models.Category>(post.Category);
                    //post.Permalink = $"~/{category.Slug}/{post.Slug}";-

                    // Add to model
                    model.Posts.Add(postModel);
                }

                return model;
            }
            return null;
        }

        /// <summary>
        /// Gets the post archive for the category with the given slug.
        /// </summary>
        /// <param name="slug">The unique category slug</param>
        /// <param name="page">The optional page</param>
        /// <param name="year">The optional year</param>
        /// <param name="month">The optional month</param>
        /// <returns>The archive model</returns>
        public Models.ArchiveModel GetBySlug(string slug, int? page = 1, int? year = null, int? month = null)
        {
            var category = db.Categories
                .SingleOrDefault(c => c.Slug == slug);

            if (category != null)
                return GetById<Models.ArchiveModel>(category.Id, page, year, month);
            return null;
        }

        /// <summary>
        /// Gets the post archive for the category with the given slug.
        /// </summary>
        /// <param name="slug">The unique category slug</param>
        /// <param name="page">The optional page</param>
        /// <param name="year">The optional year</param>
        /// <param name="month">The optional month</param>
        /// <returns>The archive model</returns>
        public T GetBySlug<T>(string slug, int? page = 1, int? year = null, int? month = null) where T: Models.ArchiveModel
        {
            var category = db.Categories
                .SingleOrDefault(c => c.Slug == slug);
            if (category != null)
                return GetById<T>(category.Id, page, year, month);
            return null;
        }
    }
}
