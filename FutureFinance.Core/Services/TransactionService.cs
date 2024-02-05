using FutureFinance.Data;
using FutureFinance.Domain;

namespace FutureFinance.Core;

public class TransactionService : ITransactionService
{
    private readonly ValidationService _validationService = new();
    private readonly MappingService _mappingService = new();
    private readonly Utilities _utilities = new();
    private readonly TransactionRepo _transactionRepo = new();
    private readonly AccountRepo _accountRepo = new();
    private readonly DispositionRepo _dispositionRepo = new();
    private readonly CustomerRepo _customerRepo = new();
    private readonly UserRepo _userRepo = new();

    public Status NewTransaction(NewTransactionRequest request, int userId)
    {
        var originAccount = _accountRepo.GetAccount(request.AccountId);
        var status = _validationService.ValidateTransaction(request);
        if (status != Status.Ok)
            return status;
        
        if (originAccount == null)
            return Status.NotFound;
        
        if (!_utilities.UserIsConnectedToAccount(userId, originAccount.AccountId))
            return Status.Unauthorized;
        
        var entity = _mappingService.ToTransactionEntity(request);
        entity.Bank = entity.Bank.ToUpper();
        entity.Date = DateTime.Now;
        entity.Type = Utilities.CreditOrDebit(entity.Amount);
        entity.Balance = originAccount.Balance + request.Amount;


        if (entity.Bank == "FF")
        {
            var inTransResponse = InternalTransaction(entity);
            if (inTransResponse != Status.Ok)
                return inTransResponse;
            
            entity.Operation = "Internal Transaction to Another Account";
        }

        _transactionRepo.NewTransaction(entity);
        return Status.Ok;
    }

    public Status InternalTransaction(TransactionEntity firstTransaction)
    {
        var receivingAccount = _accountRepo.GetAccount(firstTransaction.Account);
        if (receivingAccount == null)
            return Status.NotFound;

        TransactionEntity secondTransaction = new()
        {
            AccountId = receivingAccount.AccountId,
            Date = DateTime.Now,
            Type = Utilities.CreditOrDebit(decimal.Negate(firstTransaction.Amount)),
            Operation = "Internal Transaction from Another Account",
            Amount = decimal.Negate(firstTransaction.Amount),
            Balance = decimal.Negate(firstTransaction.Amount),
            Symbol = firstTransaction.Symbol,
            Bank = "FF",
            Account = firstTransaction.AccountId
        };

        _transactionRepo.NewTransaction(secondTransaction);
        return Status.Ok;
    }

    public GetTransactionResponse GetTransaction(int id, int userId, bool admin = false)
    {
        var transaction = _transactionRepo.GetTransaction(id);
        if (transaction == null)
            return new(Status.NotFound);

        if (!admin && !_utilities.UserIsConnectedToAccount(userId, transaction.AccountId))
            return new(status: Status.Unauthorized);
        
        return new(Status.Ok, _mappingService.ToTransactionDTO(transaction));
    }

    public GetTransactionResponse GetTransactionsFromAccount(int accountId, int userId, bool admin = false)
    {
        if (_accountRepo.GetAccount(accountId) == null)
            return new(Status.NotFound);

        if (!admin && !_utilities.UserIsConnectedToAccount(userId, accountId))
            return new(Status.Unauthorized);
        
        var transactions = _transactionRepo.GetTransactionsFromAccount(accountId);
        if (transactions.Count < 1)
            return new(Status.Empty);
        
        return new(Status.Ok, transactions: _mappingService.ToTransactionDTO(transactions));
    }
}
