using FutureFinance.Domain;

namespace FutureFinance.Core;

public interface IAccountService
{
    Status OpenAccount(NewAccountRequest request, int userId);
    GetAccountResponse GetAccount(int id, int userId, bool admin = false);
    List<AccountDTO> GetAccountsFromCustomer(int customerId);
    List<AccountDTO> GetAccountsFromUser(int userId);
}