using FutureFinance.Domain;

namespace FutureFinance.Core;

public interface ILoanService
{
    Status NewLoan(NewLoanRequest request);
    GetLoanResponse GetLoan(int id, int userId, bool admin = false);
    List<LoanDTO> GetLoansFromAccount(int accountId);
}
