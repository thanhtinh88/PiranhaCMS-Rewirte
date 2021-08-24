using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.Extend.Fields
{
    public abstract class SimpleField<T> : Field
    {
        /// <summary>
        /// Gets/sets the field value.
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// Gets the client value.
        /// </summary>
        /// <returns></returns>
        public override object GetValue()
        {
            return Value;
        }
    }
}
