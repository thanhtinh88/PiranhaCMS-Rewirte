using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Piranha.Core.AspNet
{
    /// <summary>
    /// Base class for middleware.
    /// </summary>
    public abstract class MiddlewareBase
    {
        #region Members
        /// <summary>
        /// The next middleware in the pipeline.
        /// </summary>
        protected readonly RequestDelegate next;

        /// <summary>
        /// The current api.
        /// </summary>
        protected readonly Api api;
        #endregion

        /// <summary>
        /// Creates a new middleware instance.
        /// </summary>
        /// <param name="next">The next middleware in the pipeline</param>
        public MiddlewareBase(RequestDelegate next)
        {
            this.next = next;
            this.api = new Api();
        }

        /// <summary>
        /// Invokes the middleware.
        /// </summary>
        /// <param name="context">The current http context</param>
        /// <returns>An async task</returns>
        public abstract Task Invoke(HttpContext context);
    }
}
