using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Piranha.Manager
{
    public static class ManagerModuleExtensions
    {
        /// <summary>
        /// Adds the Piranha manager module.
        /// </summary>
        /// <param name="services">The current service collection</param>
        /// <returns>The services</returns>
        public static IServiceCollection AddPiranhaManager(this IServiceCollection services)
        {
            var assembly = typeof(ManagerModuleExtensions).GetTypeInfo().Assembly;
            var provider = new EmbeddedFileProvider(assembly);

            // Add the file provider to ther Razoer view engine
            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation(options => options.FileProviders.Add(provider));

            // Add the manager module
            Piranha.App.Modules.Add(new Piranha.Manager.Module());
            return services;
        }

        public static IApplicationBuilder UsePiranhaManager(this IApplicationBuilder builder)
        {
            return builder
                .UseMiddleware<Piranha.Manager.ResourceMiddleware>();
        }
    }
}
