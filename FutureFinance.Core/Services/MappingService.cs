using FutureFinance.Data;
using FutureFinance.Domain;

namespace FutureFinance.Core;

public class MappingService : IMappingService
{
    private readonly AccountRepo _accountRepo = new();

    public UserDTO ToUserDTO(UserEntity entity)
    {
        return new()
        {
            Id = entity.Id,
            CustomerId = entity.CustomerId
        };
    }

    public List<UserDTO> ToUserDTO(List<UserEntity> entities)
    {
        List<UserDTO> dTOs = [];
        foreach (var entity in entities)
        {
            dTOs.Add(ToUserDTO(entity));
        }
        return dTOs;
    }    

    public CustomerEntity ToCustomerEntity(NewCustomerRequest request)
    {
        return new()
        {
            Gender = request.Gender,
            Givenname = request.Givenname,
            Surname = request.Surname,
            Streetaddress = request.Streetaddress,
            City = request.City,
            Zipcode = request.Zipcode,
            Country = request.Country,
            CountryCode = request.CountryCode,
            Birthday = request.Birthday,
            Telephonecountrycode = request.Telephonecountrycode,
            Telephonenumber = request.Telephonenumber,
            Emailaddress = request.Emailaddress
        };
    }

    public CustomerDTO ToCustomerDTO(CustomerEntity entity)
    {
        return new()
        {
            CustomerId = entity.CustomerId,
            Gender = entity.Gender,
            Givenname = entity.Givenname,
            Surname = entity.Surname,
            Streetaddress = entity.Streetaddress,
            City = entity.City,
            Zipcode = entity.Zipcode,
            Country = entity.Country,
            CountryCode = entity.CountryCode,
            Birthday = entity.Birthday,
            Telephonecountrycode = entity.Telephonecountrycode,
            Telephonenumber = entity.Telephonenumber,
            Emailaddress = entity.Emailaddress
        };
    }

    public List<CustomerDTO> ToCustomerDTO(List<CustomerEntity> entities)
    {
        List<CustomerDTO> DTOs = [];
        foreach (var entity in entities)
        {
            DTOs.Add(ToCustomerDTO(entity));
        }
        return DTOs;
    }

    public CustomerEntity ToCustomerEntity(UpdateCustomerRequest request)
    {
        return new()
        {
            Streetaddress = request.Streetaddress,
            City = request.City,
            Zipcode = request.Zipcode,
            Country = request.Country,
            CountryCode = request.CountryCode,
            Telephonecountrycode = request.Telephonecountrycode,
            Telephonenumber = request.Telephonenumber,
            Emailaddress = request.Emailaddress
        };
    }

    public UserEntity ToUserEntity(NewUserRequest request)
    {
        return new()
        {
            CustomerId = request.CustomerId,
            Password = request.Password
        };
    }

    public AccountEntity ToAccountEntity(NewAccountRequest request)
    {
        return new()
        {
            Frequency = request.Frequency,
            AccountTypesId = request.AccountTypesId
        };
    }

    public AccountDTO ToAccountDTO(AccountEntity entity)
    {
        return new()
        {
            AccountId = entity.AccountId,
            Frequency = entity.Frequency,
            Created = entity.Created,
            Balance = entity.Balance,
            AccountTypesId = _accountRepo.GetAccountTypes().FirstOrDefault(t =>
                t.AccountTypeId == entity.AccountTypesId)!.TypeName
        };
    }

    public List<AccountDTO> ToAccountDTO(List<AccountEntity> entities)
    {
        List<AccountDTO> DTOs = [];
        foreach (var entity in entities)
        {
            DTOs.Add(ToAccountDTO(entity));
        }
        return DTOs;
    }

    public CustomerDispositionDTO ToCustomerDispositionDTO(CustomerEntity entity)
    {
        return new()
        {
            CustomerId = entity.CustomerId,
            Gender = entity.Gender,
            Givenname = entity.Givenname,
            Surname = entity.Surname,
            Streetaddress = entity.Streetaddress,
            City = entity.City,
            Zipcode = entity.Zipcode,
            Country = entity.Country,
            CountryCode = entity.CountryCode,
            Birthday = entity.Birthday,
            Telephonecountrycode = entity.Telephonecountrycode,
            Telephonenumber = entity.Telephonenumber,
            Emailaddress = entity.Emailaddress
        };
    }

    public LoanDTO ToLoanDTO(LoanEntity entity)
    {
        return new()
        {
            LoanId = entity.LoanId,
            AccountId = entity.AccountId,
            Date = entity.Date,
            Amount = entity.Amount,
            Duration = entity.Duration,
            Payments = entity.Payments,
            Status = entity.Status
        };
    }

    public List<LoanDTO> ToLoanDTO(List<LoanEntity> entities)
    {
        List<LoanDTO> DTOs = [];
        foreach (var entity in entities)
        {
            DTOs.Add(ToLoanDTO(entity));
        }
        return DTOs;
    }

    public LoanEntity ToLoanEntity(NewLoanRequest request)
    {
        return new()
        {
            AccountId = request.Account,
            Amount = request.Amount,
            Duration = request.Duration
        };
    }

    public TransactionEntity ToTransactionEntity(NewTransactionRequest request)
    {
        return new()
        {
            AccountId = request.AccountId,
            Operation = request.Operation,
            Amount = request.Amount,
            Symbol = request.Symbol,
            Bank = request.Bank,
            Account = request.Account
        };
    }

    public TransactionDTO ToTransactionDTO(TransactionEntity entity)
    {
        return new()
        {
            TransactionId = entity.TransactionId,
            AccountId = entity.AccountId,
            Date = entity.Date,
            Type = entity.Type,
            Operation = entity.Operation,
            Amount = entity.Amount,
            Balance = entity.Balance,
            Symbol = entity.Symbol,
            Bank = entity.Bank,
            Account = entity.Account
        };
    }

    public List<TransactionDTO> ToTransactionDTO(List<TransactionEntity> entities)
    {
        List<TransactionDTO> DTOs = [];
        foreach (var entity in entities)
        {
            DTOs.Add(ToTransactionDTO(entity));
        }
        return DTOs;
    }
}
