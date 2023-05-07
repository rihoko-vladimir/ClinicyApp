using Clinicy.WebApi.Common.SqlCommands;
using Clinicy.WebApi.Interfaces.Factories;
using Clinicy.WebApi.Interfaces.Repositories;
using Clinicy.WebApi.Models.Entities;
using Dapper;
using Serilog;

namespace Clinicy.WebApi.Repositories;

public class TicketsRepository : ITicketsRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public TicketsRepository(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<Ticket?> GetTicketById(Guid ticketId)
    {
        await using var dbConnection = _dbConnectionFactory.GetConnection();

        Log.Information("Querying for ticket with id {Id}", ticketId);

        var request = TicketSqlCommand.GetTicketByIdRequest(ticketId);

        var ticket =
            await dbConnection.QueryFirstOrDefaultAsync<Ticket>(request.Query, request.DynamicParameters,
                commandTimeout: 5000);

        if (ticket is not null)
            return ticket;

        Log.Warning("Ticket wasn't found");

        return null;
    }

    public async Task<Guid> CreateNewTicket(Guid patientId, string cabinetNumber, DateTime requestedDateTime)
    {
        await using var dbConnection = _dbConnectionFactory.GetConnection();

        Log.Information("Creating new ticket for user {UserId}, Cabinet {CabinetId}, At time {RequestedDateTime}",
            patientId, cabinetNumber, requestedDateTime);

        var request = TicketSqlCommand.CreateTicketRequest(patientId, cabinetNumber, requestedDateTime);

        var guid = await dbConnection.QueryFirstOrDefaultAsync<Guid>(request.Query, request.DynamicParameters,
            commandTimeout: 5000);

        return guid;
    }

    public async Task RevokeTicket(Guid ticketId)
    {
        await using var dbConnection = _dbConnectionFactory.GetConnection();

        Log.Information("Revoking ticket {Id}", ticketId);

        var request = TicketSqlCommand.RevokeTicketRequest(ticketId);

        await dbConnection.ExecuteAsync(request.Query, request.DynamicParameters, commandTimeout: 5000);
    }
}