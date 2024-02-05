using FutureFinance.Domain;

namespace FutureFinance.Core;

public interface IMappingService
{
    UserEntity ToUserEntity(NewUserRequest request);
    UserDTO ToUserDTO(UserEntity entity);
    List<UserDTO> ToUserDTO(List<UserEntity> entities);
    CustomerEntity ToCustomerEntity(NewCustomerRequest request);
    CustomerDTO ToCustomerDTO(CustomerEntity entity);
    List<CustomerDTO> ToCustomerDTO(List<CustomerEntity> entities);
    CustomerEntity ToCustomerEntity(UpdateCustomerRequest request);
    AccountEntity ToAccountEntity(NewAccountRequest request);
    AccountDTO ToAccountDTO(AccountEntity entity);
    List<AccountDTO> ToAccountDTO(List<AccountEntity> entities);
    CustomerDispositionDTO ToCustomerDispositionDTO(CustomerEntity entity);
    LoanDTO ToLoanDTO(LoanEntity entity);
    List<LoanDTO> ToLoanDTO(List<LoanEntity> entities);
    LoanEntity ToLoanEntity(NewLoanRequest request);
    TransactionEntity ToTransactionEntity(NewTransactionRequest request);
    TransactionDTO ToTransactionDTO(TransactionEntity entity);
    List<TransactionDTO> ToTransactionDTO(List<TransactionEntity> entities);
}