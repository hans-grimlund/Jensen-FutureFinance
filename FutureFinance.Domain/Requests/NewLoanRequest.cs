namespace FutureFinance.Domain;

public class NewLoanRequest
{
    public int Account { get; set; }
    public decimal Amount { get; set; }
    public int Duration { get; set; }
}
