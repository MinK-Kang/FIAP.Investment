using System;

namespace FIAP.Domain.Investments
{
    public class InvestmentDetails
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Investimento mínimo
        /// </summary>
        public double MinimumInvestment { get; set; }

        /// <summary>
        /// Imposto de renda
        /// </summary>
        public double IncomeTax { get; set; }

        /// <summary>
        /// Nome do produto
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Descrição do produto
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Emissor
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// Período mínimo de resgate
        /// </summary>
        public DateTime MinimumRedemptionPeriod { get; set; }

        /// <summary>
        /// Tipo de investimento (Renda fixa, poupanca, fundos...)
        /// </summary>
        public InvestmentType InvestmentType { get; set; }
    }
}