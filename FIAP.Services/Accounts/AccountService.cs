using FIAP.Domain.Accounts;
using FIAP.Repositories;
using System;

namespace FIAP.Services.Accounts
{
    public class AccountService : IAccountService
    {
        private IAccountRepository accountRepository;
        private IStatementEntryRepository statementEntryRepository;

        public AccountService(IAccountRepository accountRepository,
                                IStatementEntryRepository statementEntryRepository)
        {
            this.accountRepository = accountRepository;
            this.statementEntryRepository = statementEntryRepository;
        }

        public void Transfer(Account fromAccount, Account toAccount, double value)
        {
            if (fromAccount.Balance < value)
            {
                throw new Exception("Saldo Insuficiente");
            }
            var outStatementEntry = new StatementEntry()
            {
                AccountId = fromAccount.Id,
                Date = DateTime.Now,
                MovementType = MovementType.Debit,
                Value = value
            };
            var inStatementEntry = new StatementEntry()
            {
                AccountId = toAccount.Id,
                Date = DateTime.Now,
                MovementType = MovementType.Credit,
                Value = value
            };

            DoAccountOperation(fromAccount, outStatementEntry);
            DoAccountOperation(toAccount, inStatementEntry);
        }

        private void DoAccountOperation(Account account, StatementEntry statementEntry)
        {
            if (statementEntry.MovementType == MovementType.Credit)
                account.Balance += statementEntry.Value;
            else
                account.Balance -= statementEntry.Value;

            statementEntryRepository.CreateOrUpdate(statementEntry);
            accountRepository.CreateOrUpdate(account);
        }
    }
}