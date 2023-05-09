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
    private readonly IAccessTokenService _accessTokenService;
    private readonly JwtConfiguration _jwtConfiguration;
    private readonly IPatientService _patientService;
    private readonly IRefreshTokenService _refreshTokenService;

    public AuthController(JwtConfiguration jwtConfiguration, IPatientService patientService,
        IAccessTokenService accessTokenService, IRefreshTokenService refreshTokenService)
    {
        _jwtConfiguration = jwtConfiguration;
        _patientService = patientService;
        _accessTokenService = accessTokenService;
        _refreshTokenService = refreshTokenService;
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

    [HttpPost]
    [Route("refresh")]
    public async Task<ActionResult> Refresh()
    {
        var refreshToken = this.GetRefreshTokenFromCookie();
        var accessSuccess = _accessTokenService.GetGuidFromAccessToken(refreshToken, out var userId);
        var refreshSuccess = _refreshTokenService.GetGuidFromRefreshToken(refreshToken, out var refreshUserId);

        if (!accessSuccess && !refreshSuccess) return BadRequest();

        var result = await _patientService.RefreshTokensAsync(refreshUserId);

        if (result is null) return Unauthorized();

        this.SetAccessAndRefreshCookie(result.AccessToken, result.RefreshToken, _jwtConfiguration);

        return Ok();
    }
}