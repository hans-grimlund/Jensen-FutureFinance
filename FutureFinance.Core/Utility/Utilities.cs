using FutureFinance.Data;

namespace FutureFinance.Core;

public class Utilities
{
    private readonly TransactionRepo _transactionRepo = new();
    private readonly AccountRepo _accountRepo = new();
    private readonly DispositionRepo _dispositionRepo = new();
    private readonly CustomerRepo _customerRepo = new();
    private readonly UserRepo _userRepo = new();

    public static string CreditOrDebit(decimal amount)
    {
        if (amount < 0)
            return "Debit";
        if (amount > 0)
            return "Credit";
        
        return string.Empty;
    }

    public bool UserIsConnectedToAccount(int userId, int accountId)
    {
        var user = _userRepo.GetUser(userId) ?? throw new Exception("User not found");
        var customer = _customerRepo.GetCustomer(user.CustomerId) ?? throw new Exception("Customer not found");
        var account = _accountRepo.GetAccount(accountId) ?? throw new Exception("Account not found");
        var dispositions = _dispositionRepo.GetDispositionsFromAccount(account.AccountId);
        
        var connection = dispositions.FirstOrDefault(d => d.CustomerId == customer.CustomerId);

        if (connection != null)
            return true;
        return false;
    }
}
