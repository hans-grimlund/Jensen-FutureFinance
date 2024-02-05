namespace FutureFinance.Domain;

public class GetLoanResponse
{
    public LoanDTO? Loan { get; set; }
    public Status Status { get; set; }

    public GetLoanResponse(LoanDTO? loan = null, Status? status = null)
    {
        Loan = loan ?? new();
        Status = status ?? Status.None;
    }
}
