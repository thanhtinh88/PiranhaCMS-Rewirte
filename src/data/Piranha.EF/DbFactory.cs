using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.EF
{
    public class DbContxtFactory : IDesignTimeDbContextFactory<Db>
    {
        public Db CreateDbContext(string[] args)
        {
            var optionBuilder = new DbContextOptionsBuilder<Db>();
            optionBuilder.UseSqlite("Data Source=piranha-dev.db");
            
            return new Db(optionBuilder.Options);
        }
    }
}
