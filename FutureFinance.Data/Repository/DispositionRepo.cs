using Dapper;
using FutureFinance.Domain;
using Microsoft.Data.SqlClient;

namespace FutureFinance.Data;

public class DispositionRepo : IDispositionRepo
{
    public DispositionEntity GetDisposition(int id)
    {
        using SqlConnection conn = new(ConnectionString.cs);

        DynamicParameters parameters = new();
        parameters.Add("@Id", id);

        return conn.QueryFirstOrDefault<DispositionEntity>("SelectDisposition", parameters, commandType: System.Data.CommandType.StoredProcedure)!;
    }

    public List<DispositionEntity> GetDispositionsFromAccount(int accountId)
    {
        using SqlConnection conn = new(ConnectionString.cs);

        DynamicParameters parameters = new();
        parameters.Add("@Id", accountId);

        return conn.Query<DispositionEntity>("SelectDispositionsFromAccount", parameters, commandType: System.Data.CommandType.StoredProcedure).ToList();
    }

    public List<DispositionEntity> GetDispositionsFromCustomer(int customerId)
    {
        using SqlConnection conn = new(ConnectionString.cs);

        DynamicParameters parameters = new();
        parameters.Add("@Id", customerId);

        return conn.Query<DispositionEntity>("SelectDispositionsFromCustomer", parameters, commandType: System.Data.CommandType.StoredProcedure).ToList();
    }

    public void InsertDisposition(DispositionEntity disposition)
    {
        using SqlConnection conn = new(ConnectionString.cs);

        DynamicParameters parameters = new();
        parameters.Add("@CustomerId", disposition.CustomerId);
        parameters.Add("@AccountId", disposition.AccountId);
        parameters.Add("@Type", disposition.Type);

        conn.Execute("InsertDisposition", parameters, commandType: System.Data.CommandType.StoredProcedure);
    }
}
