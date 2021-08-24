using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Piranha.EF.Repositories
{
    /// <summary>
	/// Base class for entity repositories.
	/// </summary>
	/// <typeparam name="T">The entity type</typeparam>
	/// <typeparam name="TModel">The model type</typeparam>
    public abstract class RepositoryBase<T, TModel> 
        where T: class, Data.IModel
        where TModel: class
    {
        /// <summary>
		/// The current db context.
		/// </summary>
        protected Db db;

        protected RepositoryBase(Db db)
        {
            this.db = db;
        }

        /// <summary>
		/// Gets the model with the given id.
		/// </summary>
		/// <param name="id">The unique id</param>
		/// <returns>The model</returns>
        public virtual TModel GetById(Guid id)
        {
            var result = Query().SingleOrDefault(e => e.Id == id);
            if (result != null)
            {
                return Map(result);
            }
            return null;
        }

        /// <summary>
		/// Gets the base entity query.
		/// </summary>
		/// <returns>The query</returns>
        protected virtual IQueryable<T> Query()
        {
            return db.Set<T>();
        }

        /// <summary>
		/// Maps the an entity to a model.
		/// </summary>
		/// <param name="result">The data entity</param>
		/// <returns>The model</returns>
        protected virtual TModel Map(T result)
        {
            return Module.Mapper.Map<T, TModel>(result);
        }
    }
}
