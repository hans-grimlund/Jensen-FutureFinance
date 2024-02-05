using FutureFinance.Domain;

namespace FutureFinance.Core;

public interface ICustomerService
{
    Status AddCustomer(NewCustomerRequest request);
    Status UpdateCustomer(UpdateCustomerRequest request);
    List<CustomerDTO> FindCustomer(string searchterm);
    GetCustomersResponse GetCustomersFromAccount(int accountId, int userId, bool admin = false);
}
