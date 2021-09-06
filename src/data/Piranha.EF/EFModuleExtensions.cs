using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.EF
{
    public static class EFModuleExtensions
    {
        public static IServiceCollection AddPiranhaEF(this IServiceCollection services, Action<DbContextOptionsBuilder> options, ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            // store the configuration
            Piranha.EF.Module.DbConfig = options;

            // Add the db options
            services.AddDbContext<Piranha.EF.Db>(options, lifetime);
            if (lifetime == ServiceLifetime.Scoped)
                services.AddScoped<Piranha.IApi, Piranha.EF.Api>();
            else if (lifetime == ServiceLifetime.Singleton)
                services.AddSingleton<Piranha.IApi, Piranha.EF.Api>();
            else if (lifetime == ServiceLifetime.Transient)
                services.AddTransient<Piranha.IApi, Piranha.EF.Api>();

            // Add the EF module
            Piranha.App.Modules.Add(new Piranha.EF.Module());

            return services;
        }
    }
}
