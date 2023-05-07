using Clinicy.WebApi.Interfaces.Repositories;
using Clinicy.WebApi.Interfaces.Services;
using Clinicy.WebApi.Models.Entities;

namespace Clinicy.WebApi.Services;

public class TicketsService : ITicketsService
{
    private readonly ITicketsRepository _ticketsRepository;

    public TicketsService(ITicketsRepository ticketsRepository)
    {
        _ticketsRepository = ticketsRepository;
    }

    public async Task<Ticket?> GetTicketById(Guid ticketId)
    {
        return await _ticketsRepository.GetTicketById(ticketId);
    }

    public async Task<Guid> CreateNewTicket(Guid patientId, string cabinetNumber, DateTime requestedDateTime)
    {
        return await _ticketsRepository.CreateNewTicket(patientId, cabinetNumber, requestedDateTime);
    }

    public async Task RevokeTicket(Guid ticketId)
    {
        await _ticketsRepository.RevokeTicket(ticketId);
    }
}