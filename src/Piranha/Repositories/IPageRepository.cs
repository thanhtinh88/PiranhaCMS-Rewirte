﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Piranha.Repositories
{
    public interface IPageRepository
    {
        /// <summary>
        /// Gets all of the available pages for the current site.
        /// </summary>
        /// <param name="siteId">The optional site id</param>
        /// <param name="transaction">The optional transaction</param>
        /// <returns>The pages</returns>
        IEnumerable<Models.DynamicPage> GetAll(Guid? siteId = null, IDbTransaction transaction = null);

        /// <summary>
        /// Gets the site startpage.
        /// </summary>
        /// <param name="siteId">The optional site id</param>
        /// <param name="transaction">The optional transaction</param>
        /// <returns>The page model</returns>
        Models.DynamicPage GetStartPage(Guid? siteId = null, IDbTransaction transaction = null);

        /// <summary>
        /// Gets the site startpage.
        /// </summary>
        /// <typeparam name="T">The model type</typeparam>
        /// <param name="siteId">The optional site id</param>
        /// <param name="transaction">The optional transaction</param>
        /// <returns>The page model</returns>
        T GetStartPage<T>(Guid? siteId = null, IDbTransaction transaction = null) where T : Models.Page<T>;

        /// <summary>
        /// Gets the page model with the specified id.
        /// </summary>
        /// <param name="id">The unique id</param>
        /// <param name="transaction">The optional transaction</param>
        /// <returns>The page model</returns>
        Models.DynamicPage GetById(Guid id, IDbTransaction transaction = null);

        /// <summary>
        /// Gets the page model with the specified id.
        /// </summary>
        /// <typeparam name="T">The model type</typeparam>
        /// <param name="id">The unique id</param>
        /// <param name="transaction">The optional transaction</param>
        /// <returns>The page model</returns>
        T GetById<T>(Guid id, IDbTransaction transaction = null) where T : Models.Page<T>;

        /// <summary>
        /// Gets the page model with the specified slug.
        /// </summary>
        /// <param name="slug">The unique slug</param>
        /// <param name="transaction">The optional transaction</param>
        /// <returns>The page model</returns>
        Models.DynamicPage GetBySlug(string slug, IDbTransaction transaction = null);

        /// <summary>
        /// Gets the page model with the specified slug.
        /// </summary>
        /// <typeparam name="T">The model type</typeparam>
        /// <param name="slug">The unique slug</param>
        /// <param name="transaction">The optional transaction</param>
        /// <returns>The page model</returns>
        T GetBySlug<T>(string slug, IDbTransaction transaction = null) where T : Models.Page<T>;

        /// <summary>
        /// Saves the given page model
        /// </summary>
        /// <param name="model">The page model</param>
        /// <param name="transaction">The optional transaction</param>
        void Save<T>(T Model, IDbTransaction transaction = null) where T : Models.Page<T>;

        /// <summary>
        /// Deletes the model with the specified id.
        /// </summary>
        /// <param name="id">The unique id</param>
        /// <param name="transaction">The optional transaction</param>
        void Delete(Guid id, IDbTransaction transaction = null);

        /// <summary>
        /// Deletes the given model.
        /// </summary>
        /// <param name="model">The model</param>
        /// <param name="transaction">The optional transaction</param>
        void Delete<T>(T Model, IDbTransaction transaction = null) where T : Models.Page<T>;

    }
}
