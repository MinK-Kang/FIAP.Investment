using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FIAP.Fase5.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InvestmentController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<InvestmentController> _logger;

        public InvestmentController(ILogger<InvestmentController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public void Get()
        {
        }
    }
}