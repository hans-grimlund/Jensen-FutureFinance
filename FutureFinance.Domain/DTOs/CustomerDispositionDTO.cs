﻿namespace FutureFinance.Domain;

public class CustomerDispositionDTO
{
    public int CustomerId { get; set; }
    public string Gender { get; set; } = string.Empty;
    public string Givenname { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Streetaddress { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Zipcode { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string CountryCode { get; set; } = string.Empty;
    public DateTime Birthday { get; set; }
    public string Telephonecountrycode { get; set; } = string.Empty;
    public string Telephonenumber { get; set; } = string.Empty;
    public string Emailaddress { get; set; } = string.Empty;
    public string AccountRelation { get; set; } = string.Empty;
}
