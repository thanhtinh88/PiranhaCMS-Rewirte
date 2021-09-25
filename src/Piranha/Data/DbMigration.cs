using System;
using System.Collections.Generic;
using System.Text;

namespace Piranha.Data
{
    public sealed class DbMigration
    {
        public string Name { get; set; }
        public string Script { get; set; }
    }
}
