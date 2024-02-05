using FutureFinance.Data;
using FutureFinance.Domain;
using Microsoft.IdentityModel.Tokens;

namespace FutureFinance.Core;

public class ValidationService : IValidationService
{
    private readonly AccountRepo _accountRepo = new();
    private readonly CustomerRepo _requestRepo = new();

    public Status ValidateAccount(NewAccountRequest request)
    {
        if (!request.Frequency.Equals("Weekly", StringComparison.CurrentCultureIgnoreCase) &&
            !request.Frequency.Equals("Monthly", StringComparison.CurrentCultureIgnoreCase) &&
            !request.Frequency.Equals("AfterTransaction", StringComparison.CurrentCultureIgnoreCase))
                return Status.Invalid;

        if (_accountRepo.GetAccountTypes().FirstOrDefault(a =>
                a.AccountTypeId == request.AccountTypesId) == null)
                    return Status.Invalid;
        
        return Status.Ok;
    }

    public Status ValidateCustomer(NewCustomerRequest request)
    {
        if (!request.Gender.Equals("male", StringComparison.CurrentCultureIgnoreCase) &&
            !request.Gender.Equals("female", StringComparison.CurrentCultureIgnoreCase))
                return Status.Invalid;
            
        if (string.IsNullOrEmpty(request.Givenname) || request.Givenname.Length >= 100)
            return Status.Invalid;

        if (string.IsNullOrEmpty(request.Surname) || request.Surname.Length >= 100)
            return Status.Invalid;

        if (string.IsNullOrEmpty(request.Streetaddress) || request.Streetaddress.Length >= 100)
            return Status.Invalid;

        if (string.IsNullOrEmpty(request.City) || request.City.Length >= 100)
            return Status.Invalid;

        if (string.IsNullOrEmpty(request.Zipcode) || request.Zipcode.Length <= 4 || request.Zipcode.Length >= 15)
            return Status.Invalid;

        if (string.IsNullOrEmpty(request.Country) || request.Country.Length >= 100)
            return Status.Invalid;

        if (request.CountryCode.Length != 2)
            return Status.Invalid;

        if (string.IsNullOrEmpty(request.Telephonecountrycode) || request.Telephonecountrycode.Length > 3)
                return Status.Invalid;

        if (string.IsNullOrEmpty(request.Telephonenumber) || request.Telephonenumber.Length >= 25)
                return Status.Invalid;

        if (request.Birthday < new DateTime(1900,01,01) || request.Birthday > new DateTime(2006,01,01))
            return Status.Invalid;

        if (string.IsNullOrEmpty(request.Emailaddress) || request.Emailaddress.Length <= 4 || request.Emailaddress.Length >= 100
            || !request.Emailaddress.Contains('@') || !request.Emailaddress.Contains('.'))
                return Status.Invalid;

        return Status.Ok;
    }

    public Status ValidateCustomer(UpdateCustomerRequest request)
    {
        if (string.IsNullOrEmpty(request.Streetaddress) || request.Streetaddress.Length >= 100)
            return Status.Invalid;

        if (string.IsNullOrEmpty(request.City) || request.City.Length >= 100)
            return Status.Invalid;

        if (string.IsNullOrEmpty(request.Zipcode) || request.Zipcode.Length <= 5 || request.Zipcode.Length >= 15)
            return Status.Invalid;

        if (string.IsNullOrEmpty(request.Country) || request.Country.Length >= 100)
            return Status.Invalid;

        if (request.CountryCode.Length != 2)
            return Status.Invalid;

        if (string.IsNullOrEmpty(request.Telephonecountrycode) || request.Telephonecountrycode.Length > 3)
                return Status.Invalid;

        if (string.IsNullOrEmpty(request.Telephonenumber) || request.Telephonenumber.Length >= 25)
                return Status.Invalid;

        if (string.IsNullOrEmpty(request.Emailaddress) || request.Emailaddress.Length <= 4 || request.Emailaddress.Length >= 100
            || !request.Emailaddress.Contains('@') || !request.Emailaddress.Contains('.'))
                return Status.Invalid;

        return Status.Ok;
    }

    public Status ValidateLoan(NewLoanRequest request)
    {
        if (_accountRepo.GetAccount(request.Account) == null)
            return Status.NotFound;
        
        if (request.Amount < 50000)
            return Status.Invalid;

        return request.Duration switch
        {
            12 or 24 or 36 or 48 or 60 => Status.Ok,
            _ => Status.Invalid,
        };
    }

    public Status ValidateNewUser(NewUserRequest request)
    {
        if (_requestRepo.GetCustomer(request.CustomerId) == null)
            return Status.NotFound;

        return ValidatePassword(request.Password);
    }

    public Status ValidateTransaction(NewTransactionRequest request)
    {
        if (request.Operation.IsNullOrEmpty())
            return Status.InvalidOperation;
        
        if (request.Bank.Length != 2 && !request.Bank.IsNullOrEmpty())
            return Status.InvalidBank;
        
        if (request.Amount == 0)
            return Status.InvalidAmount;
        

        return Status.Ok;
    }

    public Status ValidatePassword(string password)
    {
        if (password.Length < 8 || password.Length > 50)
            return Status.InvalidPassword;
        
        return Status.Ok;
    }
}
