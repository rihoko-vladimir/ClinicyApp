namespace Shared.Models.Messages;

public class RegisterNewPatientMessage
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string ContactNumber { get; set; }

    public string Email { get; set; }

    public string PassportNumber { get; set; }

    public char Gender { get; set; }

    public string PhotoUrl { get; set; }

    public string Password { get; set; }
}