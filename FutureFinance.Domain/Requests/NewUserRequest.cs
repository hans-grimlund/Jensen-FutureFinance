namespace FutureFinance.Domain;

public class NewUserRequest
{
    public int CustomerId { get; set; }
    public string Password { get; set; } = string.Empty;
}
