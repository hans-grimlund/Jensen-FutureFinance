namespace FutureFinance.Domain;

public class AccountDTO
{
    public int AccountId { get; set; }
    public string Frequency { get; set; } = string.Empty;
    public DateTime Created { get; set; }
    public decimal Balance { get; set; }
    public string AccountTypesId { get; set; } = string.Empty;
}
