using Clinicy.WebApi.Models.Entities;

namespace Clinicy.WebApi.Interfaces.Repositories;

public interface ITicketsRepository
{
    public Task<Ticket?> GetTicketById(Guid ticketId);

    public Task<Guid> CreateNewTicket(Guid patientId, string cabinetNumber, DateTime requestedDateTime);

    public Task RevokeTicket(Guid ticketId);
}