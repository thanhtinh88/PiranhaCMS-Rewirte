﻿using Piranha.Models;
using System;

namespace Piranha.Repositories
{
    /// <summary>
	/// The client archive repository interface.
	/// </summary>
    public interface IArchiveRepostiory
    {
        /// <summary>
        /// Gets the post archive for the category with the given id.
        /// </summary>
        /// <param name="id">The unique category id</param>
        /// <param name="page">The optional page</param>
        /// <param name="year">The optional year</param>
        /// <param name="month">The optional month</param>
        /// <returns>The archive model</returns>
		ArchiveModel GetById(Guid id, int? page = 1, int? year = null, int? month = null);

        /// <summary>
        /// Gets the post archive for the category with the given id.
        /// </summary>
        /// <param name="id">The unique category id</param>
        /// <param name="page">The optional page</param>
        /// <param name="year">The optional year</param>
        /// <param name="month">The optional month</param>
        /// <returns>The archive model</returns>
        T GetById<T>(Guid id, int? page = 1, int? year = null, int? month = null) where T : ArchiveModel;

        /// <summary>
        /// Gets the post archive for the category with the given slug.
        /// </summary>
        /// <param name="slug">The unique category slug</param>
        /// <param name="page">The optional page</param>
        /// <param name="year">The optional year</param>
        /// <param name="month">The optional month</param>
        /// <returns>The archive model</returns>
        ArchiveModel GetBySlug(string slug, int? page = 1, int? year = null, int? month = null);

        /// <summary>
        /// Gets the post archive for the category with the given slug.
        /// </summary>
        /// <param name="slug">The unique category slug</param>
        /// <param name="page">The optional page</param>
        /// <param name="year">The optional year</param>
        /// <param name="month">The optional month</param>
        /// <returns>The archive model</returns>
        T GetBySlug<T>(string slug, int? page = 1, int? year = null, int? month = null) where T : ArchiveModel;

    }
}