namespace FutureFinance.Domain;

public class LoginResponse(Status? status = null, string? jwt = null)
{
    public Status Status { get; set; } = status ?? Status.None;
    public string JWT { get; set; } = jwt ?? string.Empty;
}
