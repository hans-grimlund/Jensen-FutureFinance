namespace FutureFinance.Domain;

public class NewTransactionRequest
{
    public int AccountId { get; set; }
    public string Operation { get; set; }
    public decimal Amount { get; set; }
    public string Symbol { get; set; }
    public string Bank { get; set; }
    public int Account { get; set; }

    public NewTransactionRequest(int accountId = 0, string? operation = null,
        decimal amount = 0, string? symbol = null, string? bank = null, int account = 0)
    {
        AccountId = accountId;
        Operation = operation ?? string.Empty;
        Amount = amount;
        Symbol = symbol ?? string.Empty;
        Bank = bank ?? string.Empty;
        Account = account;
    }

    public NewTransactionRequest()
    {
        Operation ??= string.Empty;
        Symbol ??= string.Empty;
        Bank ??= string.Empty;
    }
}
