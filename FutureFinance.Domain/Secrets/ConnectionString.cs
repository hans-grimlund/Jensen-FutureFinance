namespace FutureFinance.Domain;

public class ConnectionString
{
    // public static readonly string cs = "Server=localhost;Database=FutureFinance;User Id=sa;Password=Password123!;TrustServerCertificate=True;";
    public static readonly string cs = "Server=tcp:localhost;Initial Catalog=FutureFinance;Persist Security Info=False;User ID=sa;Password=Password123!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30";
}
