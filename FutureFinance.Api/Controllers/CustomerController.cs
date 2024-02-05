using FutureFinance.Core;
using FutureFinance.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FutureFinance.Api.Controllers;

[ApiController]
[Authorize(Roles = "Admin")]
[Route("[controller]")]
public class CustomerController(IErrorhandler errorhandler, ICustomerService customerService) : ControllerBase
{
    private readonly IErrorhandler _errorhandler = errorhandler;
    private readonly ICustomerService _customerService = customerService;

    [HttpPost]
    public IActionResult NewCustomer([FromBody]NewCustomerRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        try
        {
            var status = _customerService.AddCustomer(request);
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

    [Authorize(Roles = "Standard")]
    [HttpPatch]
    public IActionResult UpdateCustomer([FromBody]UpdateCustomerRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        try
        {
            request.Id = GetCurrentUserId();
            if (request.Id == 0)
                return BadRequest();
                
            var status = _customerService.UpdateCustomer(request);
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

    [HttpGet("find")]
    public IActionResult FindCustomer([FromQuery]string searchterm)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        try
        {
            var customers = _customerService.FindCustomer(searchterm);
            if (customers.Count > 0)
                return Ok(customers);
            
            return NotFound();
        }
        catch (Exception ex)
        {
            _errorhandler.LogError(ex);
            return Problem();
        }
    }

    
    [HttpGet("dispositions")]
    public IActionResult GetCustomersFromAccount([FromQuery]int accountId)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        
        try
        {
            var response = _customerService.GetCustomersFromAccount(accountId,
                GetCurrentUserId(), User.IsInRole("Admin"));

        return response.Status switch
        {
            Status.Ok => Ok(response.Customers),
            Status.NotFound => NotFound(),
            Status.Unauthorized => Unauthorized(),
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