using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.Extend.Fields
{
    /// <summary>
    /// Base class for simple single type fields.
    /// </summary>
    /// <typeparam name="T">The field type</typeparam>
    public abstract class SimpleField<T>: Field
    {
        // <summary>
        /// Gets/sets the field value.
        /// </summary>
        public T Value { get; set; }
    }
}
