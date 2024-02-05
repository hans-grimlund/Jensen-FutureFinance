using FutureFinance.Domain;

namespace FutureFinance.Core;

public interface ITransactionService
{
    Status NewTransaction(NewTransactionRequest request, int userId);
    Status InternalTransaction(TransactionEntity transaction);
    GetTransactionResponse GetTransaction(int id, int userId, bool admin = false);
    GetTransactionResponse GetTransactionsFromAccount(int accountId, int userId, bool admin = false);
}
