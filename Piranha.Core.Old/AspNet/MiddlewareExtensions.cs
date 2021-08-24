using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Piranha.Core.AspNet
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UsePiranha(this IApplicationBuilder builder, Handle options = Handle.All)
        {
            return builder.UsePiranha(null, options);
        }

        public static IApplicationBuilder UsePiranha(this IApplicationBuilder builder, Action<AppConfig> config, Handle options = Handle.All)
        {
            App.Init(config);

            if (options.HasFlag(Handle.Pages) || options.HasFlag(Handle.All))
                builder = builder.UseMiddleware<PageMiddleware>();
            if (options.HasFlag(Handle.Posts) || options.HasFlag(Handle.All))
                builder = builder.UseMiddleware<PostMiddleware>();
            if (options.HasFlag(Handle.StartPage) || options.HasFlag(Handle.All))
                builder = builder.UseMiddleware<StartPageMiddleware>();

            return builder;
        }
    }
}
