﻿using Piranha.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Piranha.Repositories
{
    public interface ISiteRepository
    {
        /// <summary>
        /// Gets all available models.
        /// </summary>
        /// <param name="transaction">The optional transaction</param>
        /// <returns>The available models</returns>
        IEnumerable<Site> GetAll(IDbTransaction transaction = null);

        /// <summary>
        /// Gets the model with the specified id.
        /// </summary>
        /// <param name="id">The unique id</param>
        /// <param name="transaction">The optional transaction</param>
        /// <returns>The model, or NULL if it doesn't exist</returns>
        Site GetById(Guid id, IDbTransaction transaction = null);

        /// <summary>
        /// Gets the model with the specified id.
        /// </summary>
        /// <param name="id">The unique id</param>
        /// <param name="transaction">The optional transaction</param>
        /// <returns>The model, or NULL if it doesn't exist</returns>
        Site GetByInternalId(string internalId, IDbTransaction transaction = null);

        /// <summary>
        /// Gets the default side.
        /// </summary>
        /// <param name="transaction">The optional transaction</param>
        /// <returns>The modell, or NULL if it doesnt exist</returns>
        Site GetDefault(IDbTransaction transaction = null);

        /// <summary>
        /// Adds or updates the given model in the database
        /// depending on its state.
        /// </summary>
        /// <param name="model">The model</param>
        /// <param name="transaction">The optional transaction</param>
        void Save(Site model, IDbTransaction transaction = null);

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
        void Delete(Site model, IDbTransaction transaction = null);
    }
}
