using System;
using System.Collections.Generic;
using System.Text;

namespace FIAP.Domain.Accounts
{
    public class Transfer
    {
        public Int64 FromAccountId { get; set; }
        public Int64 ToAccountId { get; set; }

        public double Value { get; set; }
    }
}
