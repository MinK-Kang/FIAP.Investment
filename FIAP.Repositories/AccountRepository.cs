using FIAP.Domain.Accounts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SQLite;
using FIAP.Repositories.Extensions;

namespace FIAP.Repositories
{
    public class AccountRepository : BaseRepository<Int64, Account>, IAccountRepository
    {
        private const string tableName = "Account";

        private IStatementEntryRepository statementEntryRepository;

        public AccountRepository(IStatementEntryRepository statementEntryRepository)
        {
            this.statementEntryRepository = statementEntryRepository;
        }

        public void CreateAccountTable()
        {
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = $@"CREATE TABLE IF NOT EXISTS ACCOUNT (Id INTEGER PRIMARY KEY AUTOINCREMENT, AccountNumber varchar(8), BranchNumber varchar(4), ClientTaxId varchar(15), Balance Real, LastBalanceUpdate Integer)";
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override void CreateOrUpdate(Account entity)
        {
            try
            {

                using (var cmd = DbConnection().CreateCommand())
                {
                    if (entity.Id == 0)
                    {

                        cmd.CommandText = $"INSERT OR REPLACE INTO {tableName}(AccountNumber, BranchNumber, ClientTaxId, Balance, LastBalanceUpdate)" +
                            $"VALUES (@AccountNumber, @BranchNumber, @ClientTaxId, @Balance, @LastBalanceUpdate)";
                    }
                    else
                    {
                        cmd.CommandText = $"INSERT OR REPLACE INTO {tableName}(Id, AccountNumber, BranchNumber, ClientTaxId, Balance, LastBalanceUpdate)" +
                            $"VALUES (@Id, @AccountNumber, @BranchNumber, @ClientTaxId, @Balance, @LastBalanceUpdate)";
                    }

                    cmd.Parameters.AddWithValue("@Id", entity.Id);
                    cmd.Parameters.AddWithValue("@AccountNumber", entity.AccountNumber);
                    cmd.Parameters.AddWithValue("@BranchNumber", entity.BranchNumber);
                    cmd.Parameters.AddWithValue("@ClientTaxId", entity.ClientTaxId);
                    cmd.Parameters.AddWithValue("@Balance", entity.Balance);
                    cmd.Parameters.AddWithValue("@LastBalanceUpdate", entity.LastBalanceUpdate);

                    cmd.ExecuteNonQuery();
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override void Delete(Int64 entity)
        {
            throw new NotImplementedException();
        }

        public override Account Get(Int64 id)
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
                        var statementEntryRepository = new StatementEntryRepository();

                        return new Account
                        {
                            Id = reader.GetFieldValue<Int64>("Id"),
                            BranchNumber = reader.GetFieldValue<string>("BranchNumber"),
                            AccountNumber = reader.GetFieldValue<string>("AccountNumber"),
                            ClientTaxId = reader.GetFieldValue<string>("ClientTaxId"),
                            Balance = reader.GetFieldValue<double>("Balance"),
                            LastBalanceUpdate = DateTimeExtension.FromUnixTimeStamp(reader.GetFieldValue<Int64>("LastBalanceUpdate")),
                            StatementEntries = statementEntryRepository.GetByAccountId(id)
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

        public Account GetByBranchNumberAndAccountNumber(string branchNumber, string accountNumber)
        {
            try
            {

                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = $"SELECT * FROM {tableName} WHERE BranchNumber=@BranchNumber AND AccountNumber=@AccountNumber ";
                    cmd.Parameters.AddWithValue("@BranchNumber", branchNumber);
                    cmd.Parameters.AddWithValue("@AccountNumber", accountNumber);

                    var reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        return new Account
                        {
                            Id = reader.GetFieldValue<int>("Id"),
                            BranchNumber = reader.GetFieldValue<string>("BranchNumber"),
                            AccountNumber = reader.GetFieldValue<string>("AccountNumber"),
                            ClientTaxId = reader.GetFieldValue<string>("ClientTaxId"),
                            Balance = reader.GetFieldValue<double>("Balance"),
                            LastBalanceUpdate = DateTimeExtension.FromUnixTimeStamp(reader.GetFieldValue<int>("LastBalanceUpdate"))
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
    }
}
