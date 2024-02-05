namespace FutureFinance.Domain;

public class AccountTypeEntity
{
    public int AccountTypeId { get; set; }
    public string TypeName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
