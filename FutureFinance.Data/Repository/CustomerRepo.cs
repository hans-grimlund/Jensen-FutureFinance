using Dapper;
using FutureFinance.Domain;
using Microsoft.Data.SqlClient;

namespace FutureFinance.Data;

public class CustomerRepo : ICustomerRepo
{
    public int AddCustomer(CustomerEntity customer)
    {
        using SqlConnection conn = new(ConnectionString.cs);

        DynamicParameters parameters = new();
        parameters.Add("@Gender", customer.Gender);
        parameters.Add("@Givenname", customer.Givenname);
        parameters.Add("@Surname", customer.Surname);
        parameters.Add("@Streetaddress", customer.Streetaddress);
        parameters.Add("@City", customer.City);
        parameters.Add("@Zipcode", customer.Zipcode);
        parameters.Add("@Country", customer.Country);
        parameters.Add("@CountryCode", customer.CountryCode);
        parameters.Add("@Birthday", customer.Birthday);
        parameters.Add("@Telephonecountrycode", customer.Telephonecountrycode);
        parameters.Add("@Telephonenumber", customer.Telephonenumber);
        parameters.Add("@Emailaddress", customer.Emailaddress);

        return conn.QueryFirst<CustomerEntity>("InsertCustomer", parameters, commandType: System.Data.CommandType.StoredProcedure).CustomerId;
    }
    
    public void UpdateCustomer(CustomerEntity customer)
    {
        using SqlConnection conn = new(ConnectionString.cs);

        DynamicParameters parameters = new();
        parameters.Add("@CustomerId", customer.CustomerId);
        parameters.Add("@Streetaddress", customer.Streetaddress);
        parameters.Add("@City", customer.City);
        parameters.Add("@Zipcode", customer.Zipcode);
        parameters.Add("@Country", customer.Country);
        parameters.Add("@CountryCode", customer.CountryCode);
        parameters.Add("@Telephonecountrycode", customer.Telephonecountrycode);
        parameters.Add("@Telephonenumber", customer.Telephonenumber);
        parameters.Add("@Emailaddress", customer.Emailaddress);

        conn.Execute("UpdateCustomer", parameters, commandType: System.Data.CommandType.StoredProcedure);
    }

    public List<CustomerEntity> FindCustomer(string searchterm)
    {
        using SqlConnection conn = new(ConnectionString.cs);

        DynamicParameters parameters = new();
        parameters.Add("@Searchterm", searchterm);

        return conn.Query<CustomerEntity>("FindCustomer", parameters, commandType: System.Data.CommandType.StoredProcedure).ToList();
    }

    public CustomerEntity GetCustomer(int id)
    {
        using SqlConnection conn = new(ConnectionString.cs);

        DynamicParameters parameters = new();
        parameters.Add("@Id", id);

        return conn.QueryFirstOrDefault<CustomerEntity>("SelectCustomer", parameters, commandType: System.Data.CommandType.StoredProcedure)!;
    }

    public CustomerEntity GetCustomer(string email)
    {
        using SqlConnection conn = new(ConnectionString.cs);

        DynamicParameters parameters = new();
        parameters.Add("@Email", email);

        return conn.QueryFirstOrDefault<CustomerEntity>("SelectCustomerFromEmail", parameters, commandType: System.Data.CommandType.StoredProcedure)!;
    }
}
