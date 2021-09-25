using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.Data
{
    /// <summary>
    /// Interface for models with a Guid id.
    /// </summary>
    public interface IModel
    {
        /// <summary>
        /// Gets/sets the unique id.
        /// </summary>
        public Guid Id { get; set; }
    }
}
