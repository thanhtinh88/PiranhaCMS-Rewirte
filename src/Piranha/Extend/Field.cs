using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.Extend
{
    /// <summary>
    /// Base class for fields.
    /// </summary>
    public abstract class Field: IField
    {
        /// <summary>
        /// Initializes the field for client use.
        /// </summary>
        /// <param name="api">The current api</param>
        public virtual void Init(Api api) { }

        /// <summary>
        /// Initializes the field for manager use. This
        /// method can be used for loading additional meta
        /// data needed.
        /// </summary>
        /// <param name="api">The current api</param>
        public virtual void InitManager(Api api) { }
    }
}
