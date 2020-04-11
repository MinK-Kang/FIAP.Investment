using FIAP.Domain.Accounts;
using System;
using System.Collections.Generic;
using System.Text;

namespace FIAP.Services.Accounts
{
    public interface IAccountService
    {
        void Transfer(Account fromAccount, Account toAccount, double value);
    }
}
