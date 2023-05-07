namespace Clinicy.WebApi.Models.Entities;

public class Ticket
{
    public Guid Id { get; set; }

    public DateTime IssuedAt { get; set; }

    public Guid IssuedById { get; set; }

    public DateTime ExpiresAt { get; set; }

    public Guid CabinetId { get; set; }
}