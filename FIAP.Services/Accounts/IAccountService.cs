using FIAP.Domain.Accounts;

namespace FIAP.Services.Accounts
{
    public interface IAccountService
    {
        void Transfer(Account fromAccount, Account toAccount, double value);
    }
}