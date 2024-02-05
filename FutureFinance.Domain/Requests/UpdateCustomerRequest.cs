namespace FutureFinance.Domain;

public class UpdateCustomerRequest
{
    public int Id { get; set; }
    public string Streetaddress { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Zipcode { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string CountryCode { get; set; } = string.Empty;
    public string Telephonecountrycode { get; set; } = string.Empty;
    public string Telephonenumber { get; set; } = string.Empty;
    public string Emailaddress { get; set; } = string.Empty;
}