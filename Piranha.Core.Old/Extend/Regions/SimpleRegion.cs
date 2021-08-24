using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Piranha.Core.Extend.Regions
{
    /// <summary>
    /// Base class for simple single value regions.
    /// </summary>
    /// <typeparam name="T">The value type</typeparam>
    public abstract class SimpleRegion<T>: Extension
    {
        /// <summary>
        /// Gets/sets the value.
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// Gets the value for the client model.
        /// </summary>
        /// <returns>The value</returns>
        public override object GetValue()
        {
            return Value;
        }
    }
}
