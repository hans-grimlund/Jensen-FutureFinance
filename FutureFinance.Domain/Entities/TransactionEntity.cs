namespace FutureFinance.Domain;

public class TransactionEntity
{
    public int TransactionId { get; set; }
    public int AccountId { get; set; }
    public DateTime Date { get; set; } = new DateTime(1900, 01, 01);
    public string Type { get; set; } = string.Empty;
    public string Operation { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public decimal Balance { get; set; }
    public string Symbol { get; set; } = string.Empty;
    public string Bank { get; set; } = string.Empty;
    public int Account { get; set; }
}
