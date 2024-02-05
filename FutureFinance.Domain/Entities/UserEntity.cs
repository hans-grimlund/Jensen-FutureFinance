namespace FutureFinance.Domain;

public class UserEntity
{
    public int Id { get; set; }
    public int Role { get; set; }
    public int CustomerId { get; set; }
    public string Password { get; set; } = string.Empty;
    public DateTime DateCreated { get; set; }
    public string AdminLogin { get; set; } = string.Empty;
}
