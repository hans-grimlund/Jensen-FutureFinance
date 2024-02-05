using Dapper;
using FutureFinance.Domain;
using Microsoft.Data.SqlClient;

namespace FutureFinance.Data;

public class TransactionRepo : ITransactionRepo 
{
    public TransactionEntity GetTransaction(int id)
    {
        using SqlConnection conn = new(ConnectionString.cs);

        DynamicParameters parameters = new();
        parameters.Add("@Id", id);

        return conn.QueryFirstOrDefault<TransactionEntity>("SelectTransaction", parameters, commandType: System.Data.CommandType.StoredProcedure)!;
    }

    public List<TransactionEntity> GetTransactionsFromAccount(int accountId)
    {
        using SqlConnection conn = new(ConnectionString.cs);

        DynamicParameters parameters = new();
        parameters.Add("@Id", accountId);

        return conn.Query<TransactionEntity>("SelectTransactionsFromAccount", parameters, commandType: System.Data.CommandType.StoredProcedure).ToList();
    }

    public void NewTransaction(TransactionEntity transaction)
    {
        using SqlConnection conn = new(ConnectionString.cs);

        DynamicParameters parameters = new();
        parameters.Add("@AccountId", transaction.AccountId);
        parameters.Add("@Date", transaction.Date);
        parameters.Add("@Type", transaction.Type);
        parameters.Add("@Operation", transaction.Operation);
        parameters.Add("@Amount", transaction.Amount);
        parameters.Add("@Balance", transaction.Balance);
        parameters.Add("@Symbol", transaction.Symbol);
        parameters.Add("@Bank", transaction.Bank);
        parameters.Add("@Account", transaction.Account);

        conn.Execute("InsertTransaction", parameters, commandType: System.Data.CommandType.StoredProcedure);
    }
}
