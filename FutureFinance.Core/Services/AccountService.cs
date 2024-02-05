using FutureFinance.Data;
using FutureFinance.Domain;
using Microsoft.Identity.Client;

namespace FutureFinance.Core;

public class AccountService : IAccountService
{
    private readonly AccountRepo _accountRepo = new();
    private readonly CustomerRepo _customerRepo = new();
    private readonly DispositionRepo _dispositionRepo = new();
    private readonly UserRepo _userRepo = new();
    private readonly MappingService _mappingService = new();
    private readonly ValidationService _validationService = new();
    private readonly Utilities _utilities = new();

    public Status OpenAccount(NewAccountRequest request, int userId)
    {
        var user = _userRepo.GetUser(userId);
        var customer = _customerRepo.GetCustomer(user.CustomerId);

        var status = _validationService.ValidateAccount(request);
        if (status != Status.Ok)
            return status;
        
        var entity = _mappingService.ToAccountEntity(request);
        var newAccountId = _accountRepo.OpenAccount(entity);

        var newDisposition = new DispositionEntity()
        {
            CustomerId = customer.CustomerId,
            AccountId = newAccountId,
            Type = "OWNER"
        };

        _dispositionRepo.InsertDisposition(newDisposition);

        return Status.Ok;
    }

    public GetAccountResponse GetAccount(int id, int userId, bool admin = false)
    {
        var account = _accountRepo.GetAccount(id);
        if (account == null)
            return new(Status.NotFound);

        if (!admin && !_utilities.UserIsConnectedToAccount(userId, id))
            return new(Status.Unauthorized);

        return new(Status.Ok, _mappingService.ToAccountDTO(account));
    }

    public List<AccountDTO> GetAccountsFromCustomer(int customerId)
    {
        var dispositions = _dispositionRepo.GetDispositionsFromCustomer(customerId);
        if (dispositions.Count < 1)
            return null!;
        
        List<AccountEntity> accounts = [];
        foreach (var d in dispositions)
        {
            accounts.Add(_accountRepo.GetAccount(d.AccountId));
        }

        if (accounts.Count < 1)
            return null!;
        
        return _mappingService.ToAccountDTO(accounts);
    }

    public List<AccountDTO> GetAccountsFromUser(int userId)
    {
        var customer = _customerRepo.GetCustomer(_userRepo.GetUser(userId).CustomerId);
        return GetAccountsFromCustomer(customer.CustomerId);
    }
}
