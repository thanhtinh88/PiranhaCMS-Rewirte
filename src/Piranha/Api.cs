﻿using Dapper;
using Piranha.Data;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;

namespace Piranha
{
    public sealed class Api: IDisposable
    {
        #region Members
        /// <summary>
        /// The private db connection.
        /// </summary>
        private readonly IDbConnection conn;

        /// <summary>
        /// If the api should dispose the connection.
        /// </summary>
        private readonly bool dispose;

        /// <summary>
        /// The private model cache.
        /// </summary>
        private ICache cache;

        /// <summary>
        /// Mutex for database initialization.
        /// </summary>
        private static readonly object mutext = new object();
        #endregion

        #region Properties
        /// <summary>
        /// Gets the page repository.
        /// </summary>
        public Repositories.IPageRepository Pages { get; set; }

        /// <summary>
        /// Gets the page type repository.
        /// </summary>
        public Repositories.IPageTypeRepository PageTypes { get; set; }

        /// <summary>
        /// Gets the param repository.
        /// </summary>
        public Repositories.IParamRepository Params { get; set; }

        /// <summary>
        /// Gets the site repository.
        /// </summary>
        public Repositories.ISiteRepository Sites { get; set; }
        #endregion

        public Api(Action<DbBuilder> options, ICache modelCache = null)
        {
            var config = new DbBuilder();
            options?.Invoke(config);

            if (config.Connection != null)
            {
                conn = config.Connection;
            }
            else
            {
                conn = new SqlConnection(config.ConnectionString);
                dispose = true;
            }

            if (config.Migrate)
            {
                lock (mutext)
                {
                    if (!IsCompatible())
                        UpdateDatabase();
                }
            }

            Setup(modelCache);
        }

        /// <summary>
        /// Creates a new api on the default piranha connection string.
        /// </summary>
        /// <param name="modelCache">The optional model cache</param>
        public Api(ICache modelCache = null) :
            this(o => o.ConnectionString = ConfigurationManager.ConnectionStrings["piranha"].ConnectionString, modelCache)
        {
        }

        /// <summary>
        /// Checks if the database is compatible with the current model.
        /// </summary>
        /// <returns>If the database is up to date</returns>
        public bool IsCompatible()
        {
            return GetMigrationCount() == Db.Migrations.Length;
        }

        /// <summary>
        /// Updates the database to the latest migration.
        /// </summary>
        public void UpdateDatabase()
        {
            var pos = GetMigrationCount();

            if (pos < Db.Migrations.Length)
            {
                conn.Open();

                using (var tx = conn.BeginTransaction())
                {
                    for (int n = pos; n < Db.Migrations.Length; n++)
                    {
                        conn.Execute(GetMigrations(n), transaction: tx);

                        // Add into the migration history
                        conn.Execute("INSERT INTO [Piranha_Migrations] ([Id], [Name], [Created]) VALUES(@Id, @Name, @Created)", new
                        {
                            Id = Guid.NewGuid(),
                            Name = Db.Migrations[n].Name,
                            Created = DateTime.Now
                        }, transaction: tx);
                    }
                    tx.Commit();
                }
                conn.Close();
            }
        }

        /// <summary>
        /// Begins a new transaction.
        /// </summary>
        public IDbTransaction BeginTransaction()
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            return conn.BeginTransaction();
        }

        /// <summary>
        /// Disposes the current api.
        /// </summary>
        public void Dispose()
        {
            if (dispose)
            {
                conn.Close();
                conn.Dispose();
            }
        }

        #region private methods
        /// <summary>
        /// Configures the api.
        /// </summary>
        /// <param name="modelCache">The optional model cache</param>
        private void Setup(ICache modelCache = null)
        {
            cache = modelCache;

            Pages = new Repositories.PageRepository(this, conn, cache);
            PageTypes = new Repositories.PageTypeRepository(conn, cache);
            Params = new Repositories.ParamRepository(conn, cache);
            Sites = new Repositories.SiteRepository(conn, cache);
        }

        /// <summary>
        /// Gets the current migration count from the database.
        /// </summary>
        /// <returns>The migration count</returns>
        private int GetMigrationCount()
        {
            try
            {
                return conn.ExecuteScalar<int>("SELECT COUNT(*) FROM Piranha_Migrations");
            }
            catch { }
            return 0;
        }

        /// <summary>
        /// Gets the embedded migration at the specific position.
        /// </summary>
        /// <param name="index">The migration</param>
        /// <returns>The migration script</returns>
        private string GetMigrations(int index)
        {
            var assembly = typeof(Api).GetTypeInfo().Assembly;

            var sqlFile = Db.Migrations[index].Script;

            using (var reader = new StreamReader(assembly.GetManifestResourceStream(sqlFile)))
            {
                return reader.ReadToEnd();
            }
        }
        #endregion
    }
}
