using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using FIAP.Investment.Domain.Investments;
using FIAP.Repositories;

namespace FIAP.Investment.Repositories {
  public class InvestmentRepository : BaseRepository<int, InvestmentDetails>, IInvestmentRepository {
    private const string tableName = "Investment";

    public void CreateSQLiteBase () {
      try {
        SQLiteConnection.CreateFile ($@"{Environment.CurrentDirectory}\Investment.sqlite");
      } catch (Exception ex) {
        throw ex;
      }
    }

    public void CreateInvestmentTable () {
      try {
        using (var cmd = DbConnection ().CreateCommand ()) {
          cmd.CommandText = $@"CREATE TABLE IF NOT EXIST Investment
                                        (Id int, MinimumInvestment decimal(10,2), IncomeTax(4,2), Name varchar(80), Description varchar(80), Issuer varchar(50), MinimumRedemptionPeriod text)";
        }
      } catch (Exception ex) {
        throw ex;
      }
    }

    public override void Delete (int Id) {
      try {
        using (var cmd = DbConnection ().CreateCommand ()) {
          cmd.CommandText = $"DELETE FROM {tableName} WHERE Id = {Id}";
          cmd.ExecuteNonQuery ();
        }
      } catch (Exception ex) {
        throw ex;
      }
    }

    public override InvestmentDetails Get (int Id) {
      try {

        using (var cmd = DbConnection ().CreateCommand ()) {
          cmd.CommandText = $"SELECT * FROM {tableName} WHERE Id=@Id ";
          cmd.Parameters.AddWithValue ("@Id", Id);

          var reader = cmd.ExecuteReader ();

          while (reader.Read ()) {
            return new InvestmentDetails {
              Id = reader.GetFieldValue<int> ("Id"),
                InvestmentType = (InvestmentType) reader.GetFieldValue<int> ("InvestmentType"),
                IncomeTax = reader.GetFieldValue<double> ("IncomeTax"),
                Description = reader.GetFieldValue<string> ("Description"),
                Issuer = reader.GetFieldValue<string> ("Issuer"),
                Name = reader.GetFieldValue<string> ("Name"),
                MinimumInvestment = reader.GetFieldValue<decimal> ("MinimumInvestment"),
                MinimumRedemptionPeriod = reader.GetFieldValue<DateTime> ("MinimumRedemptionPeriod")
            };
          }

          return null;

        }
      } catch (Exception ex) {
        throw ex;
      }
    }

    public override void Insert (InvestmentDetails entity) {
      try {
        using (var cmd = DbConnection ().CreateCommand ()) {
          cmd.CommandText = $"INSERT INTO {tableName}" +
            $"(Id, MinimumInvestment, IncomeTax, Name, Description, Issuer, MinimumRedemptionPeriod) " +
            $"values" +
            $"(@Id, @MinimumInvestment, @IncomeTax, @Name, @Description, @Issuer, @MinimumRedemptionPeriod)";

          cmd.Parameters.AddWithValue ("@Id", entity.Id);
          cmd.Parameters.AddWithValue ("@MinimumInvestment", entity.MinimumInvestment);
          cmd.Parameters.AddWithValue ("@IncomeTax", entity.IncomeTax);
          cmd.Parameters.AddWithValue ("@Name", entity.Name);
          cmd.Parameters.AddWithValue ("@Description", entity.Description);
          cmd.Parameters.AddWithValue ("@Issuer", entity.Issuer);
          cmd.Parameters.AddWithValue ("@MinimumRedemptionPeriod", entity.MinimumInvestment);

          cmd.ExecuteNonQuery ();
        }
      } catch (System.Exception ex) {

        throw ex;
      }
    }

    public override void Update (InvestmentDetails entity) {
      try {
        using (var cmd = DbConnection ().CreateCommand ()) {
          cmd.CommandText = $"UPDATE {tableName}" +
            $"SET Id=@Id, MinimumInvestment=@MinimumInvestment, " +
            $"IncomeTax=@IncomeTax, Name=@Name, Description=@Description, " +
            $"Issuer=@Issuer, MinimumRedemptionPeriod=@MinimumRedemptionPeriod ";

          cmd.Parameters.AddWithValue ("@Id", entity.Id);
          cmd.Parameters.AddWithValue ("@MinimumInvestment", entity.MinimumInvestment);
          cmd.Parameters.AddWithValue ("@IncomeTax", entity.IncomeTax);
          cmd.Parameters.AddWithValue ("@Name", entity.Name);
          cmd.Parameters.AddWithValue ("@Description", entity.Description);
          cmd.Parameters.AddWithValue ("@Issuer", entity.Issuer);
          cmd.Parameters.AddWithValue ("@MinimumRedemptionPeriod", entity.MinimumInvestment);

          cmd.ExecuteNonQuery ();
        }
      } catch (System.Exception ex) {

        throw ex;
      }
    }

    public IList<InvestmentDetails> ListAll () {
      try {
        using (var cmd = DbConnection ().CreateCommand ()) {
          cmd.CommandText = "SELECT * FROM tableName";
          return FormatReaderToList (cmd.ExecuteReader ());
        }
      } catch (System.Exception ex) {
        throw ex;
      }
    }

    public IList<InvestmentDetails> ListByType (InvestmentType type) {
      try {
        using (var cmd = DbConnection ().CreateCommand ()) {
          cmd.CommandText = "SELECT * FROM tableName WHERE InvestmentType = @Type";
          cmd.Parameters.AddWithValue ("@Type", type);

          return FormatReaderToList (cmd.ExecuteReader ());
        }
      } catch (System.Exception ex) {

        throw ex;
      }
    }

    private static IList<InvestmentDetails> FormatReaderToList (SQLiteDataReader reader) {
      try {
        var resultList = new List<InvestmentDetails> ();
        while (reader.Read ()) {
          resultList.Add (new InvestmentDetails {
            Id = reader.GetFieldValue<int> ("Id"),
              InvestmentType = (InvestmentType) reader.GetFieldValue<int> ("InvestmentType"),
              IncomeTax = reader.GetFieldValue<double> ("IncomeTax"),
              Description = reader.GetFieldValue<string> ("Description"),
              Issuer = reader.GetFieldValue<string> ("Issuer"),
              Name = reader.GetFieldValue<string> ("Name"),
              MinimumInvestment = reader.GetFieldValue<decimal> ("MinimumInvestment"),
              MinimumRedemptionPeriod = reader.GetFieldValue<DateTime> ("MinimumRedemptionPeriod")
          });
        }
        return resultList;
      } catch (System.Exception ex) {

        throw ex;
      }

    }

  }
}