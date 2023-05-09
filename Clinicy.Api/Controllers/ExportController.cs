using Clinicy.WebApi.Extensions.ControllerExtensions;
using Clinicy.WebApi.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Clinicy.WebApi.Controllers;

[ApiController]
[Route("/api/export")]
public class ExportController : ControllerBase
{
    private readonly IAccessTokenService _accessTokenService;
    private readonly ISenderService _senderService;

    public ExportController(IAccessTokenService accessTokenService, ISenderService senderService)
    {
        _accessTokenService = accessTokenService;
        _senderService = senderService;
    }

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> ExportAsync()
    {
        var token = await this.GetAccessTokenAsync();
        var success = _accessTokenService.GetGuidFromAccessToken(token, out _);


        if (!success) return Unauthorized();

        await _senderService.SendExportMessageAsync();

        return Ok();
    }
}