namespace FutureFinance.Domain;

public class AccountEntity
{
    public int AccountId { get; set; }
    public string Frequency { get; set; } = string.Empty;
    public DateTime Created { get; set; }
    public decimal Balance { get; set; }
    public int AccountTypesId { get; set; }
}
