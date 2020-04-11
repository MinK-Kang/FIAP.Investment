using System;
using System.Collections.Generic;
using System.Text;

namespace FIAP.Domain.Accounts
{
    public class Account
    {
        /// <summary>
        /// Id da conta
        /// </summary>
        public Int64 Id { get; set; }

        /// <summary>
        /// Numero da Conta
        /// </summary>
        public string AccountNumber { get; set; }

        /// <summary>
        /// Numero da Agencia
        /// </summary>
        public string BranchNumber { get; set; }

        /// <summary>
        /// Numero do Documento do Cliente
        /// </summary>
        public string ClientTaxId { get; set; }

        /// <summary>
        /// Saldo
        /// </summary>
        public double Balance { get; set; }

        /// <summary>
        /// Data da Última atualização do saldo
        /// </summary>
        public DateTime LastBalanceUpdate { get; set; }

        /// <summary>
        /// Lançamentos do Extrato
        /// </summary>
        public IList<StatementEntry> StatementEntries { get; set; }
    }
}
