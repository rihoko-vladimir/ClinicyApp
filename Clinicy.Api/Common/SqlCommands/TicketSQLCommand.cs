using System.Data;
using Clinicy.WebApi.Models.Constants;
using Dapper;

namespace Clinicy.WebApi.Common.SqlCommands;

public class TicketSqlCommand
{
    private const string GetTicketById = "exec GetTicketById @ticketId";
    private const string CreateTicket = "exec CreateTicket @patientId, @cabinetId, @requestDateTime";
    private const string RevokeTicket = "exec RevokeTicket @ticketId";

    public static PreparedRequest GetTicketByIdRequest(Guid ticketId)
    {
        var dynamicParams = new DynamicParameters();

        dynamicParams.Add("ticketId", ticketId, DbType.Guid);

        return new PreparedRequest(GetTicketById, dynamicParams);
    }

    public static PreparedRequest CreateTicketRequest(Guid patientId, Guid cabinetId, DateTime requestedDateTime)
    {
        var dynamicParams = new DynamicParameters();

        dynamicParams.Add("patientId", patientId, DbType.Guid);
        dynamicParams.Add("cabinetId", cabinetId, DbType.Guid);
        dynamicParams.Add("parentsName", requestedDateTime, DbType.DateTime2);

        return new PreparedRequest(CreateTicket, dynamicParams);
    }

    public static PreparedRequest RevokeTicketRequest(Guid ticketId)
    {
        var dynamicParams = new DynamicParameters();

        dynamicParams.Add("ticketId", ticketId, DbType.Guid);

        return new PreparedRequest(RevokeTicket, dynamicParams);
    }
}