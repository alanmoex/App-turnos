using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Application.Interfaces;
using Application;
using Application.Models.Requests;
using Domain.Entities;

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
        string token = _customAuthenticationService.Authenticate(authenticationRequest);

        return Ok(token);
    }
}