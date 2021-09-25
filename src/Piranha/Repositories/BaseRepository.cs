﻿using Dapper;
using Piranha.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace Piranha.Repositories
{
    public abstract class BaseRepository<T> where T: class, IModel
    {
        #region Members
        protected static readonly bool isCreate = typeof(ICreated).GetTypeInfo().IsAssignableFrom(typeof(T));
        protected static readonly bool isModified = typeof(IModified).GetTypeInfo().IsAssignableFrom(typeof(T));
        protected readonly IDbConnection conn;
        protected readonly ICache cache;
        protected readonly string table;
        protected readonly string sort;
        #endregion

        public BaseRepository(IDbConnection connection, string tableName, string sortOrder, ICache modelCache = null)
        {
            conn = connection;
            table = tableName;
            sort = sortOrder;
            cache = modelCache;
        }

        /// <summary>
        /// Gets all available models.
        /// </summary>
        /// <param name="transaction">The optional transaction</param>
        /// <returns>The available models</returns>
        public virtual IEnumerable<T> GetAll(IDbTransaction transaction = null)
        {
            return conn.Query<T>($"SELECT * FROM [{table}] ORDER BY [{sort}]", transaction: transaction);
        }

        /// <summary>
        /// Gets the model with the specified id.
        /// </summary>
        /// <param name="id">The unique id</param>
        /// <param name="transaction">The optional transaction</param>
        /// <returns>The model, or NULL if it doesn't exist</returns>
        public virtual T GetById(Guid id, IDbTransaction transaction = null)
        {
            T model = cache != null ? cache.Get<T>(id.ToString()) : null;

            if (model == null)
            {
                model = conn.QueryFirstOrDefault<T>($"SELECT * FROM [{table}] WHERE [Id]=@Id",
                    new { Id = id }, transaction: transaction);

                if (cache != null && model != null)
                    AddToCache(model);
            }
            return model;
        }

        /// <summary>
        /// Adds or updates the given model in the database
        /// depending on its state.
        /// </summary>
        /// <param name="model">The model</param>
        /// <param name="transaction">The optional transaction</param>
        public virtual void Save(T model, IDbTransaction transaction = null)
        {
            if ((int)conn.ExecuteScalar($"SELECT COUNT([Id]) FROM [{table}] WHERE [Id]=@Id", model, transaction: transaction) == 0)
                Add(model, transaction);
            else 
                Update(model, transaction);

            if (cache != null)
                cache.Remove(model.Id.ToString());
        }

        /// <summary>
        /// Deletes the model with the specified id.
        /// </summary>
        /// <param name="id">The unique id</param>
        /// <param name="transaction">The optional transaction</param>
        public virtual void Delete(Guid id, IDbTransaction transaction = null)
        {
            conn.Execute($"DELETE FROM [{table}] WHERE [Id]=@Id",
                new { Id = id }, transaction: transaction);

            if (cache != null)
                cache.Remove(id.ToString());
        }

        /// <summary>
        /// Deletes the given model.
        /// </summary>
        /// <param name="model">The model</param>
        /// <param name="transaction">The optional transaction</param>
        public virtual void Delete(T model, IDbTransaction transaction)
        {
            Delete(model.Id);
        }

        #region Protected methods
        /// <summary>
        /// Adds a new model to the database.
        /// </summary>
        /// <param name="model">The model</param>
        /// <param name="transaction">The optional transaction</param>
        protected abstract void Add(T model, IDbTransaction transaction = null);

        /// <summary>
        /// Updates the given model in the database.
        /// </summary>
        /// <param name="model">The model</param>
        /// <param name="transaction">The optional transaction</param>
        protected abstract void Update(T model, IDbTransaction transaction = null);

        /// <summary>
        /// Prepares the model for an insert.
        /// </summary>
        /// <param name="model">The model</param>
        /// <param name="transaction">The optional transaction</param>
        protected virtual void PrepareInsert(T model, IDbTransaction transaction = null)
        {
            model.Id = model.Id != Guid.Empty ? model.Id : Guid.NewGuid();

            if (isCreate)
                ((ICreated)model).Created = DateTime.Now;

            if (isModified)
                ((IModified)model).LastModified = DateTime.Now;
        }

        /// <summary>
        /// Perpares the model for an update.
        /// </summary>
        /// <param name="model">The model</param>
        /// <param name="transaction">The optional transaction</param>
        protected virtual void PrepareUpdate(T model, IDbTransaction transaction)
        {
            if (isModified)
                ((IModified)model).LastModified = DateTime.Now;
        }

        /// <summary>
        /// Adds the given model to cache.
        /// </summary>
        /// <param name="model">The model</param>
        protected virtual void AddToCache(T model)
        {
            cache.Set(model.Id.ToString(), model);
        }

        /// <summary>
        /// Removes the given model from cache.
        /// </summary>
        /// <param name="model">The model</param>
        protected virtual void RemoveFromCache(T model)
        {
            cache.Remove(model.Id.ToString());
        }
        #endregion
    }
}
