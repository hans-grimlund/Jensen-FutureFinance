using FutureFinance.Domain;

namespace FutureFinance.Data;

public interface ICustomerRepo
{
    int AddCustomer(CustomerEntity customer);
    void UpdateCustomer(CustomerEntity customer);
    CustomerEntity GetCustomer(int id);
    CustomerEntity GetCustomer(string email);
    List<CustomerEntity> FindCustomer(string searchterm);
}
