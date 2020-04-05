using FIAP.Investment.Domain.Investments;
using FIAP.Investment.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace FIAP.Fase5.Controllers
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
        [Route("ListAll/{type}")]
        public IList<InvestmentDetails> ListAll(InvestmentType type)
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
        public void Insert(InvestmentDetails item)
        {
            investmentRepository.Insert(item);
        }
    }
}