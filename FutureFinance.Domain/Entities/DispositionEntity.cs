namespace FutureFinance.Domain;

public class DispositionEntity
{
    public int DispositionId { get; set; }
    public int CustomerId { get; set; }
    public int AccountId { get; set; }
    public string Type { get; set; } = string.Empty;
}
