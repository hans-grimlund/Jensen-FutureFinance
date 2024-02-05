namespace FutureFinance.Domain;

public class NewAccountRequest
{
    public string Frequency { get; set; } = string.Empty;
    public int AccountTypesId { get; set; }
}