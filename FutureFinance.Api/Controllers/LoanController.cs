using FutureFinance.Core;
using FutureFinance.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FutureFinance.Api.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class LoanController(IErrorhandler errorhandler, ILoanService loanService) : ControllerBase
{
    private readonly IErrorhandler _errorhandler = errorhandler;
    private readonly ILoanService _loanService = loanService;

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public IActionResult NewLoan([FromQuery]NewLoanRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        
        try
        {
            var status = _loanService.NewLoan(request);
            if (status == Status.Ok)
                return Ok();

            return BadRequest(status.ToString());
        }
        catch (Exception ex)
        {
            _errorhandler.LogError(ex);
            return Problem();
        }
    }

    [HttpGet("id")]
    public IActionResult GetLoan([FromQuery]int loanId)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        
        try
        {
            // If user is not connected to loan via 
            // customer and account or is admin, returns Unauthorized.

            var response = _loanService.GetLoan(loanId, GetCurrentUserId(), User.IsInRole("Admin"));

            return response.Status switch
            {
                Status.Ok => Ok(response.Loan),
                Status.NotFound => NotFound(),
                Status.Unauthorized => Unauthorized(),
                _ => Problem(),
            };
        }
        catch (Exception ex)
        {
            _errorhandler.LogError(ex);
            return Problem();
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("account")]
    public IActionResult GetLoansFromAccount([FromQuery]int accountId)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        
        try
        {
            var loans = _loanService.GetLoansFromAccount(accountId);
            if (loans != null)
                return Ok(loans);

            return NotFound();
        }
        catch (Exception ex)
        {
            _errorhandler.LogError(ex);
            return Problem();
        }
    }

    [Authorize(Roles = "Standard")]
    [HttpGet]
    public IActionResult GetMyLoans()
    {
        if (!ModelState.IsValid)
            return BadRequest();
        
        try
        {
            var loans = _loanService.GetLoansFromAccount(GetCurrentUserId());
            if (loans != null)
                return Ok(loans);
            
            return NotFound();
        }
        catch (Exception ex)
        {
            _errorhandler.LogError(ex);
            return Problem();
        }
    }

    private int GetCurrentUserId()
    {
        var idClaim = User.FindFirst("UserId");
        if (idClaim == null)
            return 0;

        var parsed = int.TryParse(idClaim.Value, out int id);
        if (parsed)
            return id;

        return 0;
    }
}