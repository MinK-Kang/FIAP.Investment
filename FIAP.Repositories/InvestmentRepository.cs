using System;
using System.Data;
using System.Data.SQLite;
using FIAP.Investment.Domain.Investments;
using FIAP.Repositories;

namespace FIAP.Investment.Repositories
{
    public class InvestmentRepository : BaseRepository<int, InvestmentDetails>
    {
        private const string tableName = "Investment";

        public void CreateSQLiteBase()
        {
            try
            {
                SQLiteConnection.CreateFile($@"{Environment.CurrentDirectory}\Investment.sqlite");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CreateInvestmentTable()
        {
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = $@"CREATE TABLE IF NOT EXIST Investment
                                        (Id int, MinimumInvestment decimal(10,2), IncomeTax(4,2), Name varchar(80), Description varchar(80), Issuer varchar(50), MinimumRedemptionPeriod text)";
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
                    cmd.CommandText = $"DELETE FROM {tableName} WHERE Id = {Id}";
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override DataTable Get(int Id)
        {
            try
            {
                var dt = new DataTable();

                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = $"SELECT * FROM {tableName} WHERE Id=@Id ";
                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.ExecuteNonQuery();

                    var da = new SQLiteDataAdapter(cmd.CommandText, DbConnection());
                    da.Fill(dt);

                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override void Insert(InvestmentDetails entity)
        {
            using (var cmd = DbConnection().CreateCommand())
            {
                cmd.CommandText = $"INSERT INTO {tableName}" +
                                  $"(Id, MinimumInvestment, IncomeTax, Name, Description, Issuer, MinimumRedemptionPeriod) " +
                                  $"values" +
                                  $"(@Id, @MinimumInvestment, @IncomeTax, @Name, @Description, @Issuer, @MinimumRedemptionPeriod)";

                cmd.Parameters.AddWithValue("@Id", entity.Id);
                cmd.Parameters.AddWithValue("@MinimumInvestment", entity.MinimumInvestment);
                cmd.Parameters.AddWithValue("@IncomeTax", entity.IncomeTax);
                cmd.Parameters.AddWithValue("@Name", entity.Name);
                cmd.Parameters.AddWithValue("@Description", entity.Description);
                cmd.Parameters.AddWithValue("@Issuer", entity.Issuer);
                cmd.Parameters.AddWithValue("@MinimumRedemptionPeriod", entity.MinimumInvestment);

                cmd.ExecuteNonQuery();
            }
        }

        public override void Update(InvestmentDetails entity)
        {
            using (var cmd = DbConnection().CreateCommand())
            {
                cmd.CommandText = $"UPDATE {tableName}" +
                                  $"SET Id=@Id, MinimumInvestment=@MinimumInvestment, " +
                                  $"IncomeTax=@IncomeTax, Name=@Name, Description=@Description, " +
                                  $"Issuer=@Issuer, MinimumRedemptionPeriod=@MinimumRedemptionPeriod ";

                cmd.Parameters.AddWithValue("@Id", entity.Id);
                cmd.Parameters.AddWithValue("@MinimumInvestment", entity.MinimumInvestment);
                cmd.Parameters.AddWithValue("@IncomeTax", entity.IncomeTax);
                cmd.Parameters.AddWithValue("@Name", entity.Name);
                cmd.Parameters.AddWithValue("@Description", entity.Description);
                cmd.Parameters.AddWithValue("@Issuer", entity.Issuer);
                cmd.Parameters.AddWithValue("@MinimumRedemptionPeriod", entity.MinimumInvestment);

                cmd.ExecuteNonQuery();
            }
        }
    }
}