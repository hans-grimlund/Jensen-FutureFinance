using Dapper;
using FutureFinance.Domain;
using Microsoft.Data.SqlClient;

namespace FutureFinance.Data;

public class UserRepo : IUserRepo
{
    public void AddUser(UserEntity user)
    {
        using SqlConnection conn = new(ConnectionString.cs);

        DynamicParameters parameters = new();
        parameters.Add("@CustomerId", user.CustomerId);
        parameters.Add("@Password", user.Password);
        parameters.Add("@DateCreated", user.DateCreated);

        conn.Execute("InsertUser", parameters, commandType: System.Data.CommandType.StoredProcedure);
    }

    public void UpdatePassword(string password, int userId)
    {
        using SqlConnection conn = new(ConnectionString.cs);

        DynamicParameters parameters = new();
        parameters.Add("@Id", userId);
        parameters.Add("@Password", password);

        conn.Execute("UpdatePassword", parameters, commandType: System.Data.CommandType.StoredProcedure);
    }

    public void DeleteUser(int userId)
    {
        using SqlConnection conn = new(ConnectionString.cs);

        DynamicParameters parameters = new();
        parameters.Add("@Id", userId);

        conn.Execute("DeleteUser", parameters, commandType: System.Data.CommandType.StoredProcedure);
    }

    public List<UserEntity> GetAllUsers()
    {
        using SqlConnection conn = new(ConnectionString.cs);
        return conn.Query<UserEntity>("SelectAllUsers", commandType: System.Data.CommandType.StoredProcedure).ToList();
    }

    public UserEntity GetUser(int userId)
    {
        using SqlConnection conn = new(ConnectionString.cs);

        DynamicParameters parameters = new();
        parameters.Add("@Id", userId);

        return conn.QueryFirstOrDefault<UserEntity>("SelectUser", parameters, commandType: System.Data.CommandType.StoredProcedure)!;
    }

    public UserEntity GetUserFromCustomerId(int customerId)
    {
        using SqlConnection conn = new(ConnectionString.cs);

        DynamicParameters parameters = new();
        parameters.Add("@Id", customerId);

        return conn.QueryFirstOrDefault<UserEntity>("SelectUserFromCustomer", parameters, commandType: System.Data.CommandType.StoredProcedure)!;
    }

    public UserEntity GetAdmin(string adminlogin)
    {
        using SqlConnection conn = new(ConnectionString.cs);

        DynamicParameters parameters = new();
        parameters.Add("@AdminLogin", adminlogin);

        return conn.QueryFirstOrDefault<UserEntity>("SelectUserFromAdminLogin", parameters, commandType: System.Data.CommandType.StoredProcedure)!;
    }
}
