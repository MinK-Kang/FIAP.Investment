using System;
using System.Collections.Generic;
using System.Text;

namespace FIAP.Domain.Accounts
{
    public class StatementEntry
    {

        /// <summary>
        /// Id do Lançamento
        /// </summary>
        public Int64 Id { get; set; }


        /// <summary>
        /// Id da Conta
        /// </summary>
        public Int64 AccountId { get; set; }

        /// <summary>
        /// Data do Lançamento
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Tipo de movimentação
        /// </summary>
        public MovementType MovementType { get; set; }

        /// <summary>
        /// Valor
        /// </summary>
        public double Value { get; set; }

    }
}
