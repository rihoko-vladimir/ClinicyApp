using Shared.Models.Constants;

namespace Clinicy.Auth.Models;

public class AccountCredentials
{
    public Guid Id { get; set; } = new();

    public string Email { get; set; }

    public string PasswordHash { get; set; }

    public bool IsEmailConfirmed { get; set; }

    public string? EmailConfirmationCode { get; set; }

    public string AccountRole { get; set; } = RoleTypes.Patient;
}