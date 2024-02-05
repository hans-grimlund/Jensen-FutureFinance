using FutureFinance.Domain;

namespace FutureFinance.Core;

public interface IValidationService
{
    Status ValidateNewUser(NewUserRequest request);
    Status ValidateCustomer(NewCustomerRequest request);
    Status ValidateCustomer(UpdateCustomerRequest request);
    Status ValidateAccount(NewAccountRequest request);
    Status ValidateLoan(NewLoanRequest request);
    Status ValidateTransaction(NewTransactionRequest request);
    Status ValidatePassword(string password);
}