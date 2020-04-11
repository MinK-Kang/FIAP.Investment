using FIAP.Domain.Accounts;
using FIAP.Repositories.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SQLite;
using System.Data.Common;

namespace FIAP.Repositories
{
    public class StatementEntryRepository : BaseRepository<int, StatementEntry>, IStatementEntryRepository
    {

        private string tableName = "StatementEntry";

        public void CreateStatementEntryTable()
        {
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = $@"CREATE TABLE IF NOT EXISTS STATEMENTENTRY (Id INTEGER PRIMARY KEY, AccountId INTEGER, EntryDate REAL, MovementType INTEGER, Value REAL)";
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public override void CreateOrUpdate(StatementEntry entity)
        {
            try
            {

                using (var cmd = DbConnection().CreateCommand())
                {
                    if (entity.Id == 0)
                    {
                        cmd.CommandText = $"INSERT INTO {tableName}(AccountId, EntryDate, MovementType, Value)" +
                            $"VALUES (@AccountId, @EntryDate, @MovementType, @Value)";
                    }
                    cmd.Parameters.AddWithValue("@AccountId", entity.AccountId);
                    cmd.Parameters.AddWithValue("@EntryDate", entity.Date.ToUnixTimeStamp());
                    cmd.Parameters.AddWithValue("@MovementType", (int) entity.MovementType);
                    cmd.Parameters.AddWithValue("@Value", entity.Value);

                    cmd.ExecuteNonQuery();
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override void Delete(int entity)
        {
            throw new NotImplementedException();
        }

        public override StatementEntry Get(int id)
        {
            try
            {

                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = $"SELECT * FROM {tableName} WHERE Id=@Id ";
                    cmd.Parameters.AddWithValue("@Id", id);

                    var reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        return ToEntity(reader);
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IList<StatementEntry> GetByAccountId(Int64 accountId)
        {
            try
            {

                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = $"SELECT * FROM {tableName} WHERE AccountId=@Id ";
                    cmd.Parameters.AddWithValue("@Id", accountId);

                    var reader = cmd.ExecuteReader();

                    IList<StatementEntry> ret = new List<StatementEntry>();

                    while (reader.Read())
                    {
                        ret.Add(ToEntity(reader));
                    }

                    return ret;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static StatementEntry ToEntity(DbDataReader reader)
        {
            return new StatementEntry
            {
                Id = reader.GetFieldValue<Int64>("Id"),
                AccountId = reader.GetFieldValue<Int64>("AccountId"),
                Date = DateTimeExtension.FromUnixTimeStamp(reader.GetFieldValue<Int64>("EntryDate")),
                MovementType = (MovementType)reader.GetFieldValue<int>("MovementType"),
                Value = reader.GetFieldValue<double>("Value"),

            };

        }


    }
}
