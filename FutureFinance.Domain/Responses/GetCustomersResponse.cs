namespace FutureFinance.Domain;

public class GetCustomersResponse(List<CustomerDispositionDTO>? customers = null, Status? status = null)
{
    public List<CustomerDispositionDTO> Customers { get; set; } = customers ?? [];
    public Status Status { get; set; } = status ?? Status.None;
}
