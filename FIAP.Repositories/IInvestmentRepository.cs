using FIAP.Domain.Investments;
using System.Collections.Generic;

namespace FIAP.Repositories
{
    public interface IInvestmentRepository : IRepository<int, InvestmentDetails>
    {
        void CreateSQLiteBase();

        void CreateInvestmentTable();

        IList<InvestmentDetails> ListAll();

        IList<InvestmentDetails> ListByType(InvestmentType type);
    }
}