using FutureFinance.Domain;

namespace FutureFinance.Data;

public interface ILoanRepo
{
    void NewLoan(LoanEntity loan);
    LoanEntity GetLoan(int id);
    List<LoanEntity> GetLoansFromAccount(int accountId);
}
