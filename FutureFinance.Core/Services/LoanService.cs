using FutureFinance.Data;
using FutureFinance.Domain;

namespace FutureFinance.Core;

public class LoanService : ILoanService
{
    private readonly LoanRepo _loanRepo = new();
    private readonly AccountRepo _accountRepo = new();
    private readonly DispositionRepo _dispositionRepo = new();
    private readonly CustomerRepo _customerRepo = new();
    private readonly UserRepo _userRepo = new();
    private readonly MappingService _mappingService = new();
    private readonly ValidationService _validationService = new();
    private readonly Utilities _utilities = new();

    public Status NewLoan(NewLoanRequest request)
    {
        var status = _validationService.ValidateLoan(request);
        if (status != Status.Ok)
            return status;
        
        var entity = _mappingService.ToLoanEntity(request);
        entity.Date = DateTime.Now;

        _loanRepo.NewLoan(entity);
        return Status.Ok;
    }

    public GetLoanResponse GetLoan(int id, int userId, bool admin = false)
    {
        var loan = _loanRepo.GetLoan(id);

        if (loan == null)
            return new(status: Status.NotFound);
        
        if (!admin && _utilities.UserIsConnectedToAccount(userId, loan.AccountId))
            return new(status: Status.Unauthorized);
        
        return new(_mappingService.ToLoanDTO(loan), Status.Ok);
    }

    public List<LoanDTO> GetLoansFromAccount(int accountId)
    {
        if (_accountRepo.GetAccount(accountId) == null)
            return null!;

        var loans = _loanRepo.GetLoansFromAccount(accountId);
        if (loans == null)
            return null!;
        return _mappingService.ToLoanDTO(loans);
    }
}
