using FutureFinance.Core;

using FutureFinance.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FutureFinance.Api.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class TransactionController(IErrorhandler errorhandler, ITransactionService transactionService) : ControllerBase
{
    private readonly IErrorhandler _errorhandler = errorhandler;
    private readonly ITransactionService _transactionService = transactionService;

    [Authorize(Roles = "Standard")]
    [HttpPost]
    public IActionResult NewTransaction([FromBody]NewTransactionRequest request)
    {
        try
        {
            var status = _transactionService.NewTransaction(request, GetCurrentUserId());
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

    [HttpGet]
    public IActionResult GetTransaction([FromQuery]int transactionId)
    {
        try
        {
            var response = _transactionService.GetTransaction(transactionId,
                GetCurrentUserId(), User.IsInRole("Admin"));
            
            return response.Status switch
            {
                Status.NotFound => NotFound("Transaction not found"),
                Status.Unauthorized => Unauthorized(),
                Status.Ok => Ok(response.Transaction),
                _ => Problem()
            };
        }
        catch (Exception ex)
        {
            _errorhandler.LogError(ex);
            return Problem();
        }
    }

    [HttpGet("account")]
    public IActionResult GetTransactionsFromAccount([FromQuery]int accountId)
    {
        try
        {
            var response = _transactionService.GetTransactionsFromAccount(accountId,
                GetCurrentUserId(), User.IsInRole("Admin"));
            
            return response.Status switch
            {
                Status.NotFound => NotFound("Account not found"),
                Status.Unauthorized => Unauthorized(),
                Status.Empty => NotFound("No transactions found"),
                Status.Ok => Ok(response.Transactions),
                _ => Problem()
            };
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