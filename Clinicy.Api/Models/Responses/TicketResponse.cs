namespace Clinicy.WebApi.Models.Responses;

public class TicketResponse
{
    public Guid Id { get; set; }

    public DateTime IssuedAt { get; set; }

    public Guid IssuedById { get; set; }

    public DateTime ExpiresAt { get; set; }

    public Guid CabinetId { get; set; }
}