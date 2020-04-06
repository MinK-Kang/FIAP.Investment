using System;
using System.Data.SQLite;

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

        public abstract TEntity Get(TId id);

        public abstract void Insert(TEntity entity);

        public abstract void CreateOrUpdate(TEntity entity);

        public abstract void Update(TEntity entity);
    }
}