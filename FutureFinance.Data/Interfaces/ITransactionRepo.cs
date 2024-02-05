using FutureFinance.Domain;

namespace FutureFinance.Data;

public interface ITransactionRepo
{
    void NewTransaction(TransactionEntity transaction);
    TransactionEntity GetTransaction(int id);
    List<TransactionEntity> GetTransactionsFromAccount(int accountId);
}
