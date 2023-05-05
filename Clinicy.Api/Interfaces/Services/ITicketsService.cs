using Clinicy.WebApi.Models.Entities;

namespace Clinicy.WebApi.Interfaces.Services;

public interface ITicketsService
{
    public Task<Ticket?> GetTicketById(Guid ticketId);

    public Task<Guid> CreateNewTicket(Guid patientId, Guid cabinetId, DateTime requestedDateTime);

    public Task RevokeTicket(Guid ticketId);
}