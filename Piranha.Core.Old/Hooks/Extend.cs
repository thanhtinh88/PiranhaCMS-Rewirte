using Piranha.Core.Extend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Piranha.Core.Hooks
{
    /// <summary>
    /// The hooks available for extensions.
    /// </summary>
    public static class Extend
    {
        /// <summary>
        /// The delegates used.
        /// </summary>
        public static class Delegates
        {
            /// <summary>
            /// Delegate for adding types to the extension manager composition.
            /// </summary>
            /// <param name="mgr">composition.</param>
            public delegate void OnComposeDelegate(ExtensionManager mgr);
        }

        /// <summary>
        /// Called when the extension manager has composed the default types.
        /// </summary>
        public static Delegates.OnComposeDelegate OnCompose;
    }
}
