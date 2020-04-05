using System.Collections.Generic;
using System.Data;
using FIAP.Investment.Domain.Investments;

namespace FIAP.Investment.Repositories {
  public interface IInvestmentRepository : IRepository<int, InvestmentDetails> {
    void CreateSQLiteBase ();
    void CreateInvestmentTable ();

    IList<InvestmentDetails> ListAll ();

    IList<InvestmentDetails> ListByType (InvestmentType type);

  }
}