using FIAP.Domain.Accounts;
using FIAP.Repositories;
using FIAP.Services.Accounts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace FIAP.API.Controllers
{
    [ApiController]
    [Route("account")]
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountRepository accountRepository;
        private readonly IStatementEntryRepository statementEntryRepository;
        private readonly IAccountService accountService;

        public AccountController(
            ILogger<AccountController> logger,
            IAccountRepository accountRepository,
            IAccountService accountService,
            IStatementEntryRepository statementEntryRepository)
        {
            _logger = logger;
            this.accountRepository = accountRepository;
            this.accountService = accountService;
            this.statementEntryRepository = statementEntryRepository;
        }

        [HttpGet]
        [Route("{id}")]
        public Account Get(int id)
        {
            return this.accountRepository.Get(id);
        }

        [HttpGet]
        [Route("GetByBranchNumberAndAccountNumber/{branchNumber}/{accountNumber}")]
        public Account GetByBranchNumberAndAccountNumber(string branchNumber, string accountNumber)
        {
            return this.accountRepository.GetByBranchNumberAndAccountNumber(branchNumber, accountNumber);
        }

        [HttpPost]
        public void CreateOrUpdate(Account item)
        {
            accountRepository.CreateOrUpdate(item);
        }

        [HttpPost]
        [Route("Transfer")]
        public void Transfer(Transfer transfer)
        {
            Account fromAccount = accountRepository.Get(transfer.FromAccountId);
            Account toAccount = accountRepository.Get(transfer.ToAccountId);

            accountService.Transfer(fromAccount, toAccount, transfer.Value);
        }

        [HttpGet]
        [Route("CreateTable")]
        public void CreateTable()
        {
            accountRepository.CreateAccountTable();
            statementEntryRepository.CreateStatementEntryTable();
        }
    }
}