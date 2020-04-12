using FIAP.Domain.Investments;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace FIAP.Repositories
{
    public class InvestmentRepository : BaseRepository<int, InvestmentDetails>, IInvestmentRepository
    {
        private const string tableName = "Investment";

        public void CreateInvestmentTable()
        {
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = $@"CREATE TABLE IF NOT EXISTS INVESTMENT (Id INTEGER PRIMARY KEY, InvestmentType INTERGER, MinimumInvestment REAL, IncomeTax REAL, Name varchar(80), Description varchar(80), Issuer varchar(50), MinimumRedemptionPeriod text)";
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override void CreateOrUpdate(InvestmentDetails entity)
        {
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = $"INSERT OR REPLACE INTO {tableName}(Id, MinimumInvestment, IncomeTax, Name, Description, Issuer, MinimumRedemptionPeriod, InvestmentType)" +
                        $"VALUES (@Id, @MinimumInvestment, @IncomeTax, @Name, @Description, @Issuer, @MinimumRedemptionPeriod, @InvestmentType)";

                    cmd.Parameters.AddWithValue("@Id", entity.Id);
                    cmd.Parameters.AddWithValue("@InvestmentType", entity.InvestmentType);
                    cmd.Parameters.AddWithValue("@MinimumInvestment", entity.MinimumInvestment);
                    cmd.Parameters.AddWithValue("@IncomeTax", entity.IncomeTax);
                    cmd.Parameters.AddWithValue("@Name", entity.Name);
                    cmd.Parameters.AddWithValue("@Description", entity.Description);
                    cmd.Parameters.AddWithValue("@Issuer", entity.Issuer);
                    cmd.Parameters.AddWithValue("@MinimumRedemptionPeriod", entity.MinimumRedemptionPeriod);

                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override InvestmentDetails Get(int Id)
        {
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = $"SELECT * FROM {tableName} WHERE Id=@Id ";
                    cmd.Parameters.AddWithValue("@Id", Id);

                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        return new InvestmentDetails
                        {
                            Id = reader.GetFieldValue<int>("Id"),
                            InvestmentType = (InvestmentType)reader.GetFieldValue<int>("InvestmentType"),
                            IncomeTax = reader.GetFieldValue<double>("IncomeTax"),
                            Description = reader.GetFieldValue<string>("Description"),
                            Issuer = reader.GetFieldValue<string>("Issuer"),
                            Name = reader.GetFieldValue<string>("Name"),
                            MinimumInvestment = reader.GetFieldValue<double>("MinimumInvestment"),
                            MinimumRedemptionPeriod = reader.GetFieldValue<DateTime>("MinimumRedemptionPeriod")
                        };
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IList<InvestmentDetails> ListAll()
        {
            try
            {

                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = $"SELECT * FROM {tableName}";
                    return FormatReaderToList(cmd.ExecuteReader());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IList<InvestmentDetails> ListByType(InvestmentType type)
        {
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = $"SELECT * FROM {tableName} WHERE InvestmentType = @Type";
                    cmd.Parameters.AddWithValue("@Type", type);

                    return FormatReaderToList(cmd.ExecuteReader());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override void Delete(int Id)
        {
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = $"DELETE FROM {tableName} WHERE Id=@Id ";
                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static IList<InvestmentDetails> FormatReaderToList(SqliteDataReader reader)
        {
            try
            {
                var resultList = new List<InvestmentDetails>();
                while (reader.Read())
                {
                    resultList.Add(new InvestmentDetails
                    {
                        Id = (int)reader.GetFieldValue<Int64>("Id"),
                        InvestmentType = (InvestmentType)reader.GetFieldValue<Int64>("InvestmentType"),
                        IncomeTax = reader.GetFieldValue<double>("IncomeTax"),
                        Description = reader.GetFieldValue<string>("Description"),
                        Issuer = reader.GetFieldValue<string>("Issuer"),
                        Name = reader.GetFieldValue<string>("Name"),
                        MinimumInvestment = reader.GetFieldValue<double>("MinimumInvestment"),
                        MinimumRedemptionPeriod = DateTime.Parse(reader.GetFieldValue<string>("MinimumRedemptionPeriod"))
                    });
                }
                return resultList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}