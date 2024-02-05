namespace FutureFinance.Domain;

public class GetAccountResponse
{
    public Status Status { get; set; }
    public AccountDTO Account { get; set; }

    public GetAccountResponse(Status status = Status.None, AccountDTO? account = null)
    {
        Status = status;
        Account = account ?? new();
    }
    
    public GetAccountResponse()
    {
        Account ??= new();
    }
}
