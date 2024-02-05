using FutureFinance.Domain;

namespace FutureFinance.Data;

public interface IAccountRepo
{
    int OpenAccount(AccountEntity account);
    AccountEntity GetAccount(int id);
    List<AccountTypeEntity> GetAccountTypes();
}
