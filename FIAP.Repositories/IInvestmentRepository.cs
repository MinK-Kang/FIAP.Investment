using FIAP.Investment.Domain.Investments;
using System.Collections.Generic;

namespace FIAP.Investment.Repositories
{
    public interface IInvestmentRepository : IRepository<int, InvestmentDetails>
    {
        void CreateSQLiteBase();

        void CreateInvestmentTable();

        IList<InvestmentDetails> ListAll();

        IList<InvestmentDetails> ListByType(InvestmentType type);
    }
}