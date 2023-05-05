using Clinicy.Auth.Extensions.ControllerExtensions;
using Clinicy.Auth.Models.Request;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Models.Configurations;

namespace Clinicy.Auth.Controllers;

[ApiController]
[Route("/api/auth")]
public class AuthController : ControllerBase
{
    private readonly JwtConfiguration _jwtConfiguration;

    public AuthController(JwtConfiguration jwtConfiguration)
    {
        _jwtConfiguration = jwtConfiguration;
    }

    [HttpPost]
    [Route("login")]
    public async Task<ActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        var result = await _userService.LoginAsync(loginRequest.Login, loginRequest.Password);

        this.SetAccessAndRefreshCookie(result.Value!.AccessToken, result.Value!.RefreshToken, _jwtConfiguration);

        return Ok();
    }
}