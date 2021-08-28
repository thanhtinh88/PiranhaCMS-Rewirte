using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.EF
{
    public class DbContextFactory : IDesignTimeDbContextFactory<Db>
    {
        public Db CreateDbContext(string[] args)
        {
            var optionBuilder = new DbContextOptionsBuilder<Db>();
            var connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Database=piranha.blog;Trusted_Connection=True;";
            optionBuilder.UseSqlServer(connectionString, b => b.MigrationsAssembly("Piranha.EF"));
            return new Db(optionBuilder.Options);
        }
    }
}
