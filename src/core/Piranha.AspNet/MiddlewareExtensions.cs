﻿using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Piranha.AspNet
{
    public static class MiddlewareExtensions
    {
        /// <summary>
        /// Uses the piranha archive middleware.
        /// </summary>
        /// <param name="builder">The current application builder</param>
        /// <returns>The builder</returns>
        public static IApplicationBuilder UsePiranhaArchives(this IApplicationBuilder builder)
        {
            return builder
                .UseMiddleware<Piranha.AspNet.ArchiveMiddleware>();
        }

        /// <summary>
        /// Uses the piranha page middleware.
        /// </summary>
        /// <param name="builder">The current application builder</param>
        /// <returns>The builder</returns>
        public static IApplicationBuilder UsePiranhaPages(this IApplicationBuilder builder)
        {
            return builder
                .UseMiddleware<Piranha.AspNet.PageMiddleware>();
        }

        /// <summary>
        /// Uses the piranha post middleware.
        /// </summary>
        /// <param name="builder">The current application builder</param>
        /// <returns>The builder</returns>
        public static IApplicationBuilder UsePiranhaPosts(this IApplicationBuilder builder)
        {
            return builder
                .UseMiddleware<Piranha.AspNet.PostMiddleware>();
        }

        /// <summary>
        /// Uses the piranha startpage middleware.
        /// </summary>
        /// <param name="builder">The current application builder</param>
        /// <returns>The builder</returns>
        public static IApplicationBuilder UsePiranhaStartPage(this IApplicationBuilder builder)
        {
            return builder
                .UseMiddleware<Piranha.AspNet.StartPageMiddleware>();
        }

        /// <summary>
        /// Uses the piranha middleware.
        /// </summary>
        /// <param name="builder">The current application builder</param>
        /// <returns>The builder</returns>
        public static IApplicationBuilder UsePiranha(this IApplicationBuilder builder)
        {
            return builder
                .UseMiddleware<Piranha.AspNet.PageMiddleware>()
                .UseMiddleware<Piranha.AspNet.PostMiddleware>()
                .UseMiddleware<Piranha.AspNet.ArchiveMiddleware>()
                .UseMiddleware<Piranha.AspNet.StartPageMiddleware>();
        }
    }
}
