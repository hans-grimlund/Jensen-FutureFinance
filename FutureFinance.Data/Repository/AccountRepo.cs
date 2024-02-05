using Dapper;
using FutureFinance.Domain;
using Microsoft.Data.SqlClient;

namespace FutureFinance.Data;

public class AccountRepo : IAccountRepo
{
    public AccountEntity GetAccount(int id)
    {
        using SqlConnection conn = new(ConnectionString.cs);

        DynamicParameters parameters = new();
        parameters.Add("@Id", id);

        return conn.QueryFirstOrDefault<AccountEntity>("SelectAccount", parameters, commandType: System.Data.CommandType.StoredProcedure)!;
    }

    public List<AccountTypeEntity> GetAccountTypes()
    {
        using SqlConnection conn = new(ConnectionString.cs);

        return conn.Query<AccountTypeEntity>("SelectAllAccountTypes", commandType: System.Data.CommandType.StoredProcedure).ToList();
    }

    public int OpenAccount(AccountEntity account)
    {
        using SqlConnection conn = new(ConnectionString.cs);

        DynamicParameters parameters = new();
        parameters.Add("@Frequency", account.Frequency);
        parameters.Add("@Created", DateTime.Now);
        parameters.Add("@AccountTypesId", account.AccountTypesId);

        return conn.QueryFirst<AccountEntity>("InsertAccount", parameters, commandType: System.Data.CommandType.StoredProcedure).AccountId;
    }
}
