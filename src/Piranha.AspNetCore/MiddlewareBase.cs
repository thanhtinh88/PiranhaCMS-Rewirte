using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Piranha.AspNetCore
{
    public abstract class MiddlewareBase
    {
        #region Members
        protected readonly RequestDelegate next;
        #endregion

        /// <summary>
        /// Creates a new middleware instance.
        /// </summary>
        /// <param name="next">The next middleware in the pipeline</param>
        public MiddlewareBase(RequestDelegate next)
        {
            this.next = next;
        }

        /// <summary>
        /// Invokes the middleware.
        /// </summary>
        /// <param name="context">The current http context</param>
        /// <returns>An async task</returns>
        public abstract Task Invoke(HttpContext context, Api api);

        /// <summary>
        /// Checks if the request has already been handled by another Piranha middleware.
        /// </summary>
        /// <param name="context">The current http context</param>
        /// <returns>If the request has already been handled</returns>
        protected bool IsHandled(HttpContext context)
        {
            var values = context.Request.Query["piranha_handled"];
            if (values.Count > 0)
                return values[0] == "true";
            return false;
        }
    }
}
