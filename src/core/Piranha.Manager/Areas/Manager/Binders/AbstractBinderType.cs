using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Piranha.Areas.Manager.Binders
{
    /// <summary>
    /// A type used when trying to bind an abstract type
    /// withing the Piranha manager.
    /// </summary>
    public sealed class AbstractBinderType
    {
        /// <summary>
        /// The type.
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// The model binder.
        /// </summary>
        public IModelBinder Binder { get; set; }
    }
}
