using AutoMapper;
using Clinicy.WebApi.CustomFilters;
using Clinicy.WebApi.Extensions.ControllerExtensions;
using Clinicy.WebApi.Interfaces.Services;
using Clinicy.WebApi.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Constants;

namespace Clinicy.WebApi.Controllers;

[ApiController]
[RoleCheck(RoleTypes.Patient)]
[Route("/api/tickets")]
public class TicketsController : ControllerBase
{
    private readonly IAccessTokenService _accessTokenService;
    private readonly IMapper _mapper;
    private readonly ITicketsService _ticketsService;

    public TicketsController(ITicketsService ticketsService, IMapper mapper, IAccessTokenService accessTokenService)
    {
        _ticketsService = ticketsService;
        _mapper = mapper;
        _accessTokenService = accessTokenService;
    }

    [HttpGet]
    [Route("findById")]
    public async Task<IActionResult> FindById([FromQuery] string ticketId)
    {
        var token = await this.GetAccessTokenAsync();
        var success = _accessTokenService.GetGuidFromAccessToken(token, out _);

        if (!success) return Unauthorized();

        var ticket = await _ticketsService.GetTicketById(Guid.Parse(ticketId));

        return Ok(_mapper.Map<TicketResponse>(ticket));
    }

    [HttpPost]
    [Route("schedule")]
    public async Task<IActionResult> ScheduleTicket([FromQuery] string cabinetNumber,
        DateTime requestedDateTime)
    {
        var token = await this.GetAccessTokenAsync();
        var success = _accessTokenService.GetGuidFromAccessToken(token, out var patientId);

        if (!success) return Unauthorized();

        var id = await _ticketsService.CreateNewTicket(patientId, cabinetNumber, requestedDateTime);

        return Ok(id);
    }

    [HttpDelete]
    [Route("revoke")]
    public async Task<IActionResult> RevokeTicket([FromQuery] string ticketId)
    {
        var token = await this.GetAccessTokenAsync();
        var success = _accessTokenService.GetGuidFromAccessToken(token, out _);

        if (!success) return Unauthorized();

        await _ticketsService.RevokeTicket(Guid.Parse(ticketId));

        return Ok();
    }
}