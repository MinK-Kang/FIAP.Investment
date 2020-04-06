using FIAP.Domain.Investments;
using FIAP.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace FIAP.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InvestmentController : ControllerBase
    {
        private readonly ILogger<InvestmentController> _logger;
        private readonly IInvestmentRepository investmentRepository;

        public InvestmentController(
            ILogger<InvestmentController> logger,
            IInvestmentRepository investmentRepository)
        {
            _logger = logger;
            this.investmentRepository = investmentRepository;
        }

        [HttpGet]
        [Route("{id}")]
        public InvestmentDetails Get(int id)
        {
            return investmentRepository.Get(id);
        }

        [HttpGet]
        [Route("ListByType/{type}")]
        public IList<InvestmentDetails> ListByType(InvestmentType type)
        {
            return investmentRepository.ListByType(type);
        }

        [HttpGet]
        [Route("ListAll")]
        public IList<InvestmentDetails> ListAll()
        {
            return investmentRepository.ListAll();
        }

        [HttpDelete]
        [Route("{id}")]
        public void Delete(int id)
        {
            investmentRepository.Delete(id);
        }

        [HttpPost]
        public void CreateOrUpdate(InvestmentDetails item)
        {
            investmentRepository.CreateOrUpdate(item);
        }

        [HttpGet]
        [Route("CreateTable")]
        public void CreateTable()
        {
            investmentRepository.CreateInvestmentTable();
        }

        [HttpGet]
        [Route("CreateSqliteBase")]
        public void CreateSqliteBase()
        {
            investmentRepository.CreateSQLiteBase();
        }
    }
}