using Piranha.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Piranha.Tests
{
    public abstract class BaseTests: IDisposable
    {
        /// <summary>
        /// The default test db options.
        /// </summary>
        protected Action<DbBuilder> options = o =>
        {
            o.Connection = new SqlConnection("data source=(localdb)\\MSSQLLocalDB;initial catalog=piranha.dapper.tests;integrated security=true");
            o.Migrate = true;
        };

        /// <summary>
        /// Default constructor.
        /// </summary>
        public BaseTests()
        {
            Init();
        }

        /// <summary>
        /// Sets up & initializes the tests.
        /// </summary>
        protected abstract void Init();

        /// <summary>
        /// Disposes the test class.
        /// </summary>
        public void Dispose()
        {
            Cleanup();
        }

        /// <summary>
        /// Cleans up any possible data and resources
        /// created by the test.
        /// </summary>
        protected abstract void Cleanup();
    }
}
