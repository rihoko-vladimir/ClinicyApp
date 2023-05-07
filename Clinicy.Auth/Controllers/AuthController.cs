using Clinicy.Auth.Extensions.ControllerExtensions;
using Clinicy.Auth.Interfaces.Services;
using Clinicy.Auth.Models.Request;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Models.Configurations;

namespace Clinicy.Auth.Controllers;

[ApiController]
[Route("/api/auth")]
public class AuthController : ControllerBase
{
    private readonly JwtConfiguration _jwtConfiguration;
    private readonly IPatientService _patientService;

    public AuthController(JwtConfiguration jwtConfiguration, IPatientService patientService)
    {
        _jwtConfiguration = jwtConfiguration;
        _patientService = patientService;
    }

    [HttpPost]
    [Route("login")]
    public async Task<ActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        var result = await _patientService.LoginPatientAsync(loginRequest.Login, loginRequest.Password);

        if (result is null) return BadRequest();

        this.SetAccessAndRefreshCookie(result.AccessToken, result.RefreshToken, _jwtConfiguration);

        return Ok();
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterPatientRequest registerPatientRequest)
    {
        await _patientService.CreatePatientAsync(registerPatientRequest);

        return Ok();
    }
}