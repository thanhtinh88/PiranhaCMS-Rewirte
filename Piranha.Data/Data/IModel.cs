using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.Data.Data
{
    /// <summary>
    /// Interface for data models with a unique Guid id.
    /// </summary>
    public interface IModel
    {
        /// <summary>
        /// Gets/sets the unique id.
        /// </summary>
        public Guid Id { get; set; }
    }
}
