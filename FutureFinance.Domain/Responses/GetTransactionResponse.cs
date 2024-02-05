namespace FutureFinance.Domain;

public class GetTransactionResponse
{
    public Status Status { get; set; }
    public TransactionDTO Transaction { get; set; }
    public List<TransactionDTO> Transactions { get; set; }

    public GetTransactionResponse(Status status = Status.None, TransactionDTO? transaction = null, List<TransactionDTO>? transactions = null)
    {
        Status = status;
        Transaction = transaction ?? new();
        Transactions = transactions ?? [];
    }

    public GetTransactionResponse()
    {
        Transaction ??= new();
        Transactions ??= [];
    }
}
