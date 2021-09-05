using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.EF
{
    public static class EFModuleExtensions
    {
        public static IServiceCollection AddPiranhaEF(this IServiceCollection services, Action<DbContextOptionsBuilder> options)
        {
            // Add the db options
            services.AddDbContext<Piranha.EF.Db>(options);
            services.AddScoped<Piranha.IApi, Piranha.EF.Api>();

            // Add the EF module
            Piranha.App.Modules.Add(new Piranha.EF.Module());

            return services;
        }
    }
}
