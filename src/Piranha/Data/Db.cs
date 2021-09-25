using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.Data
{
    public static class Db
    {
        public static readonly DbMigration[] Migrations = new DbMigration[]
        {
            new DbMigration()
            {
                Name = "InitialCreate",
                Script = "Piranha.Data.Migrations.1.sql"
            }
        };

        public static void AddPiranhaDb(this IServiceCollection services, Action<DbBuilder> options)
        {
            services.AddSingleton<Action<DbBuilder>>(options);
        }
    }
}
