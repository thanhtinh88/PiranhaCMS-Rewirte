﻿using Piranha.Core.Models;
using System;

namespace Piranha.Core.Repositories
{
    /// <summary>
	/// The client archive repository interface.
	/// </summary>
    public interface IArchiveRepostiory
    {
		/// <summary>
		/// Gets the archive model for the specified category & period.
		/// </summary>
		/// <param name="id">The unique category id</param>
		/// <param name="page">The optional archive page</param>
		/// <param name="year">The optional year</param>
		/// <param name="month">The optional month</param>
		/// <returns>The model</returns>
		ArchiveModel GetByCategoryId(Guid id, int? page = null, int? year = null, int? month = null);
    }
}