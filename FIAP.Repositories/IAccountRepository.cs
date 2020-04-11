using FIAP.Domain.Accounts;
using System;
using System.Collections.Generic;
using System.Text;

namespace FIAP.Repositories
{
    public interface IAccountRepository: IRepository<Int64, Account>
    {
        void CreateAccountTable();

        Account GetByBranchNumberAndAccountNumber(string branchNumber, string accountNumber);

    }
}
