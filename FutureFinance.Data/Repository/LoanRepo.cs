using Dapper;
using FutureFinance.Domain;
using Microsoft.Data.SqlClient;

namespace FutureFinance.Data;

public class LoanRepo : ILoanRepo
{
    public LoanEntity GetLoan(int id)
    {
        using SqlConnection conn = new(ConnectionString.cs);

        DynamicParameters parameters = new();
        parameters.Add("@Id", id);

        return conn.QueryFirstOrDefault<LoanEntity>("SelectLoan", parameters, commandType: System.Data.CommandType.StoredProcedure)!;
    }

    public List<LoanEntity> GetLoansFromAccount(int accountId)
    {
        using SqlConnection conn = new(ConnectionString.cs);

        DynamicParameters parameters = new();
        parameters.Add("@Id", accountId);

        return conn.Query<LoanEntity>("SelectLoansFromAccount", parameters, commandType: System.Data.CommandType.StoredProcedure).ToList();
    }

    public void NewLoan(LoanEntity loan)
    {
        using SqlConnection conn = new(ConnectionString.cs);

        DynamicParameters parameters = new();

        parameters.Add("@AccountId", loan.AccountId);
        parameters.Add("@Date", loan.Date);
        parameters.Add("@Amount", loan.Amount);
        parameters.Add("@Duration", loan.Duration);

        conn.Execute("InsertLoan", parameters, commandType: System.Data.CommandType.StoredProcedure);
    }
}
