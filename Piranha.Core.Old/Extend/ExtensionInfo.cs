using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Piranha.Core.Extend
{
    /// <summary>
    /// Gets/sets the CLR type.
    /// </summary>
    public sealed class ExtensionInfo
    {
        #region Properties
        /// <summary>
        /// Gets/sets the CLR type.
        /// </summary>
        public Type CLRType { get; set; }

        /// <summary>
        /// Gets/sets the extension types.
        /// </summary>
        public ExtensionType Types { get; set; }
        #endregion

        internal ExtensionInfo() { }

        /// <summary>
        /// Creates a new instance of the extension.
        /// </summary>
        /// <returns></returns>
        public IExtension CreateInstance()
        {
            return (IExtension)Activator.CreateInstance(CLRType);
        }
    }
}
