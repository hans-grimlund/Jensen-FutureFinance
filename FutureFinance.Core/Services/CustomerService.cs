using FutureFinance.Data;
using FutureFinance.Domain;

namespace FutureFinance.Core;

public class CustomerService : ICustomerService
{
    private readonly CustomerRepo _customerRepo = new();
    private readonly DispositionRepo _dispositionRepo = new();
    private readonly ValidationService _validationService = new();
    private readonly UserRepo _userRepo = new();
    private readonly AccountRepo _accountRepo = new();
    private readonly MappingService _mappingService = new();

    public Status AddCustomer(NewCustomerRequest request)
    {
        var status = _validationService.ValidateCustomer(request);
        if (status != Status.Ok)
            return status;
        
        if (_customerRepo.GetCustomer(request.Emailaddress) != null)
            return Status.Forbidden;

        request.CountryCode = request.CountryCode.ToUpper();

        var entity = _mappingService.ToCustomerEntity(request);
        var newCustomerId = _customerRepo.AddCustomer(entity);

        var newAccount = new AccountEntity()
        {
            Frequency = "Monthly",
            Created = DateTime.Now,
            AccountTypesId = 1,
        };

        var newAccountId = _accountRepo.OpenAccount(newAccount);

        var newDisposition = new DispositionEntity()
        {
            CustomerId = newCustomerId,
            AccountId = newAccountId,
            Type = "OWNER"
        };

        _dispositionRepo.InsertDisposition(newDisposition);
        return Status.Ok;
    }

    public List<CustomerDTO> FindCustomer(string searchterm)
    {
        if (string.IsNullOrEmpty(searchterm))
            return null!;
        
        var entities = _customerRepo.FindCustomer(searchterm);
        if (entities.Count < 1)
            return null!;

        return _mappingService.ToCustomerDTO(entities);
    }

    public Status UpdateCustomer(UpdateCustomerRequest request)
    {
        var status = _validationService.ValidateCustomer(request);
        if (status != Status.Ok)
            return status;
        
        var entity = _mappingService.ToCustomerEntity(request);
        _customerRepo.UpdateCustomer(entity);
        
        return Status.Ok;
    }

    public GetCustomersResponse GetCustomersFromAccount(int accountId, int userId, bool admin = false)
    {
        var currentCustomer = _customerRepo.GetCustomer(_userRepo.GetUser(userId).CustomerId);
        var dispositions = _dispositionRepo.GetDispositionsFromAccount(accountId);
        if (dispositions.Count < 1)
            return new(status: Status.NotFound);

        List<CustomerDispositionDTO> customers = [];
        foreach (var d in dispositions)
        {
            var entity = _customerRepo.GetCustomer(d.CustomerId);
            var customer = _mappingService.ToCustomerDispositionDTO(entity);
            customer.AccountRelation = d.Type;
            customers.Add(customer);
        }

        if (!admin && customers.FirstOrDefault(c => c.CustomerId == currentCustomer.CustomerId) == null)
            return new(status: Status.Unauthorized);

        return new(customers, Status.Ok);
    }
}
