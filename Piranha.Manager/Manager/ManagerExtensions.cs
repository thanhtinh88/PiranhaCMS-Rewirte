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
    public static class ManagerExtensions
    {
        public static IServiceCollection AddPiranhaManager(this IServiceCollection services)
        {
            var assembly = typeof(ManagerExtensions).GetTypeInfo().Assembly;
            var provider = new EmbeddedFileProvider(assembly);

            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation(options => options.FileProviders.Add(provider));
            return services;
        }
    }
}
