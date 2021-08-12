using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Piranha.Core.AspNet
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UsePiranha(this IApplicationBuilder builder, Startup options = Startup.All)
        {
            return builder.UsePiranha(null, options);
        }

        public static IApplicationBuilder UsePiranha(this IApplicationBuilder builder, Action<AppConfig> config, Startup options = Startup.All)
        {
            App.Init(config);

            if (options.HasFlag(Startup.Pages) || options.HasFlag(Startup.All))
                builder = builder.UseMiddleware<PageMiddleware>();
            if (options.HasFlag(Startup.Posts) || options.HasFlag(Startup.All))
                builder = builder.UseMiddleware<PageMiddleware>();
            if (options.HasFlag(Startup.StartPage) || options.HasFlag(Startup.All))
                builder = builder.UseMiddleware<PageMiddleware>();

            return builder;
        }
    }
}
