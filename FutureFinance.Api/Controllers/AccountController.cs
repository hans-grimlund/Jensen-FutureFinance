using FutureFinance.Core;
using FutureFinance.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FutureFinance.Api.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class AccountController(IErrorhandler errorhandler, IAccountService accountService) : ControllerBase
{
    private readonly IErrorhandler _errorhandler = errorhandler;
    private readonly IAccountService _accountService = accountService;

    [Authorize(Roles = "Standard")]
    [HttpPost]
    public IActionResult OpenAccount([FromQuery]NewAccountRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        
        try
        {
            var status = _accountService.OpenAccount(request, GetCurrentUserId());
            if (status != Status.Ok)
                return BadRequest(status.ToString());
                
            return Ok();
        }
        catch (Exception ex)
        {
            _errorhandler.LogError(ex);
            return Problem();
        }
    }

    [Authorize(Roles = "Standard")]
    [HttpGet]
    public IActionResult GetMyAccounts()
    {
        if (!ModelState.IsValid)
            return BadRequest();
        
        try
        {
            var userId = GetCurrentUserId();
            if (userId == 0)
                return BadRequest();

            var accounts = _accountService.GetAccountsFromUser(userId);
            return Ok(accounts);
        }
        catch (Exception ex)
        {
            _errorhandler.LogError(ex);
            return Problem();
        }
    }

    [HttpGet("id")]
    public IActionResult GetAccount([FromQuery]int id)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        
        try
        {
            var response = _accountService.GetAccount(id, GetCurrentUserId(), User.IsInRole("Admin"));
            if (response.Status == Status.Ok)
                return Ok(response.Account);

            return BadRequest(response.Status.ToString());
        }
        catch (Exception ex)
        {
            _errorhandler.LogError(ex);
            return Problem();
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("fromcustomer")]
    public IActionResult GetAccountsFromCustomer([FromQuery]int customerId)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        
        try
        {
            var accounts = _accountService.GetAccountsFromCustomer(customerId);
            if (accounts != null)
                return Ok(accounts);

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