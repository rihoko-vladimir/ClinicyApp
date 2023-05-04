using Clinicy.WebApi.Models;
using Clinicy.WebApi.Models.Entities;

namespace Clinicy.WebApi.Interfaces.Repositories;

public interface ITicketsRepository
{
    public Task<Ticket> GetTicketById(Guid ticketId);

    public Task<Ticket> CreateNewTicket(Guid patientId, Guid cabinetId, DateTime requestedDateTime);

    public Task RevokeTicket(Guid ticketId);
}