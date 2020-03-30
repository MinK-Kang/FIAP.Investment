using FIAP.Investment.Repositories;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Text;

namespace FIAP.Repositories
{
    public abstract class BaseRepository<TId, TEntity> : IRepository<TId, TEntity>
    {
        public static SQLiteConnection DbConnection()
        {
            var sqliteConnection = new SQLiteConnection(
                $"Data Source={Environment.CurrentDirectory}/Investment.sqlite;");

            sqliteConnection.Open();
            return sqliteConnection;
        }

        public abstract void Delete(TId entity);

        public abstract DataTable Get(TId id);

        public abstract void Insert(TEntity entity);

        public abstract void Update(TEntity entity);
    }
}