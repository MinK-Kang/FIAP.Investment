using FIAP.Domain.Accounts;
using System;
using System.Collections.Generic;
using System.Text;

namespace FIAP.Repositories
{
    public interface IStatementEntryRepository : IRepository<int, StatementEntry>
    {
        void CreateStatementEntryTable();

        IList<StatementEntry> GetByAccountId(Int64 accountId);
    }
}
