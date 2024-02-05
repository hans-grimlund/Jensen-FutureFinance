using FutureFinance.Core;
using FutureFinance.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace FutureFinance.Api.Controllers;

[ApiController]
[Authorize(Roles = "Admin")]
[Route("[controller]")]
public class UserController(IErrorhandler errorhandler, IUserService userService) : ControllerBase
{
    private readonly IErrorhandler _errorhandler = errorhandler;
    private readonly IUserService _userService = userService;

    [AllowAnonymous]
    [HttpPost]
    public IActionResult AddUser([FromBody]NewUserRequest newUser)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        try
        {
            var status = _userService.AddUser(newUser);
            if (status == Status.Ok)
                return Ok();

            return BadRequest(status.ToString());
        }
        catch (System.Exception ex)
        {
            _errorhandler.LogError(ex);
            return Problem();
        }
    }

    [AllowAnonymous]
    [HttpGet("login")]
    public IActionResult Login([FromQuery]LoginRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        try
        {
            var response = _userService.Login(request.Email, request.Password);
            if (response.Status == Status.Ok)
                return Ok(response.JWT);

            return BadRequest(response.Status.ToString());
        }
        catch (System.Exception ex)
        {
            _errorhandler.LogError(ex);
            return Problem();
        }
    }

    [HttpDelete]
    public IActionResult DeleteUser([FromQuery]int userId)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        try
        {
            var status = _userService.DeleteUser(userId);
            if (status == Status.Ok)
                return Ok();

            return BadRequest(status.ToString());
        }
        catch (System.Exception ex)
        {
            _errorhandler.LogError(ex);
            return Problem();
        }
    }

    [HttpGet]
    public IActionResult GetUser([FromQuery]int userId)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        try
        {
            var user = _userService.GetUser(userId);
            if (user != null)
                return Ok(user);
            
            return NotFound();
        }
        catch (System.Exception ex)
        {
            _errorhandler.LogError(ex);
            return Problem();
        }
    }

    [HttpGet("getall")]
    public IActionResult GetAllUsers()
    {
        if (!ModelState.IsValid)
            return BadRequest();

        try
        {
            var users = _userService.GetAllUsers();
            if (users != null)
                return Ok(users);
            
            return NotFound();
        }
        catch (System.Exception ex)
        {
            _errorhandler.LogError(ex);
            return Problem();
        }
    }

    [HttpGet("customer")]
    public IActionResult GetUserFromCustomer([FromQuery]int customerid)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        try
        {
            var user = _userService.GetUserFromCustomerId(customerid);
            if (user != null)
                return Ok(user);
            
            return NotFound();
        }
        catch (System.Exception ex)
        {
            _errorhandler.LogError(ex);
            return Problem();
        }
    }

    [Authorize]
    [HttpPatch("password")]
    public IActionResult ChangePassword([FromQuery]string newPassword)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        try
        {
            var status = _userService.UpdatePassword(newPassword, GetCurrentUserId());
            if (status == Status.Ok)
                return Ok();

            return BadRequest(status.ToString());
        }
        catch (System.Exception ex)
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