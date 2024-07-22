using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Application.Models.Requests;
using Domain.Exceptions;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]

public class AuthenticationController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly ICustomAuthenticationService _customAuthenticationService;

    public AuthenticationController(IConfiguration configuration, ICustomAuthenticationService customAuthenticationService)
    {
        _configuration = configuration;
        _customAuthenticationService = customAuthenticationService;
    }

    [HttpPost("authenticate")]
    public ActionResult<string> Authenticate(AuthenticationRequest authenticationRequest)
    {        
        try
        {
            string token = _customAuthenticationService.Authenticate(authenticationRequest);
            return Ok(token);
        }
        catch (NotAllowedException ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An unexpected error occurred.");
        }

    }
}