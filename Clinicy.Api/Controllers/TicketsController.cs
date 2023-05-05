using AutoMapper;
using Clinicy.WebApi.Interfaces.Services;
using Clinicy.WebApi.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Clinicy.WebApi.Controllers;

[ApiController]
[Route("/api/tickets")]
public class TicketsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ITicketsService _ticketsService;

    public TicketsController(ITicketsService ticketsService, IMapper mapper)
    {
        _ticketsService = ticketsService;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("findById")]
    public async Task<IActionResult> FindById([FromQuery] string ticketId)
    {
        var ticket = await _ticketsService.GetTicketById(Guid.Parse(ticketId));

        return Ok(_mapper.Map<TicketResponse>(ticket));
    }

    [HttpPost]
    [Route("schedule")]
    public async Task<IActionResult> ScheduleTicket([FromQuery] Guid patientId, [FromQuery] Guid cabinetId,
        DateTime requestedDateTime)
    {
        var id = await _ticketsService.CreateNewTicket(patientId, cabinetId, requestedDateTime);

        return Ok(id);
    }

    [HttpDelete]
    [Route("revoke")]
    public async Task<IActionResult> RevokeTicket([FromQuery] string ticketId)
    {
        await _ticketsService.RevokeTicket(Guid.Parse(ticketId));

        return Ok();
    }
}