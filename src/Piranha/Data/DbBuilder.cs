using System.Data;

namespace Piranha.Data
{
    public sealed class DbBuilder
    {
        public string ConnectionString { get; set; }
        public IDbConnection Connection { get; set; }
        public bool Migrate { get; set; }
    }
}
