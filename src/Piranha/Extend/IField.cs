using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.Extend
{
    /// <summary>
    /// Interface for fields.
    /// </summary>
    public interface IField
    {
        /// <summary>
        /// Initializes the field for client use.
        /// </summary>
        /// <param name="api">The current api</param>
        void Init(Api api);

        /// <summary>
        /// Initializes the field for manager use. This
        /// method can be used for loading additional meta
        /// data needed.
        /// </summary>
        /// <param name="api">The current api</param>
        void InitManager(Api api);
    }
}
