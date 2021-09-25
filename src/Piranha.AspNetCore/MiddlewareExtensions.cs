using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.AspNetCore
{
    public static class MiddlewareExtensions
    {
        /// <summary>
        /// Uses the piranha middleware.
        /// </summary>
        /// <param name="builder">The current application builder</param>
        /// <returns>The builder</returns>
        public static IApplicationBuilder UsePiranha(this IApplicationBuilder builder)
        {
            return builder
                .UseMiddleware<Piranha.AspNetCore.PageMiddleware>()
                .UseMiddleware<Piranha.AspNetCore.StartPageMiddleware>();
        }

        /// <summary>
        /// Uses the piranha page middleware.
        /// </summary>
        /// <param name="builder">The current application builder</param>
        /// <returns>The builder</returns>
        public static IApplicationBuilder UsePiranhaPages(this IApplicationBuilder builder)
        {
            return builder
                .UseMiddleware<Piranha.AspNetCore.PageMiddleware>();
        }

        /// <summary>
        /// Uses the piranha startpage middleware.
        /// </summary>
        /// <param name="builder">The current application builder</param>
        /// <returns>The builder</returns>
        public static IApplicationBuilder UsePiranhaStartPage(this IApplicationBuilder builder)
        {
            return builder
                .UseMiddleware<Piranha.AspNetCore.StartPageMiddleware>();
        }
    }
}
