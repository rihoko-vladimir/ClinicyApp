namespace Clinicy.Auth.Models;

public class PatientCredentials
{
    public Guid Id { get; set; } = new();

    public string Email { get; set; }

    public string PasswordHash { get; set; }

    public bool IsEmailConfirmed { get; set; }

    public string? EmailConfirmationCode { get; set; }
}