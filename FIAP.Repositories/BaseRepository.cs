using Microsoft.Data.Sqlite;
using System;
using System.Data.SQLite;

namespace FIAP.Repositories
{
    public abstract class BaseRepository<TId, TEntity> : IRepository<TId, TEntity>
    {

        private static SqliteConnection connection;

        public static SqliteConnection DbConnection()
        {
            if (connection == null)
            { 
                connection = new SqliteConnection($"Data Source={Environment.CurrentDirectory}/Investment.sqlite;");
            }
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
            return connection;
        }

        public abstract void Delete(TId entity);

        public abstract TEntity Get(TId id);

        public abstract void CreateOrUpdate(TEntity entity);
    }
}