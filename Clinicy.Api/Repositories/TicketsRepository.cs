using Clinicy.WebApi.Interfaces.Repositories;
using Clinicy.WebApi.Models;
using Clinicy.WebApi.Models.Entities;

namespace Clinicy.WebApi.Repositories;

public class TicketsRepository : ITicketsRepository
{
    public Task<Ticket> GetTicketById(Guid ticketId)
    {
        throw new NotImplementedException();
    }

    public Task<Ticket> CreateNewTicket(Guid patientId, Guid cabinetId, DateTime requestedDateTime)
    {
        throw new NotImplementedException();
    }

    public Task RevokeTicket(Guid ticketId)
    {
        throw new NotImplementedException();
    }
}