namespace Clinicy.WebApi.Models.Responses;

public class PatientResponse
{
    public Guid Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string ContactNumber { get; set; }

    public string Email { get; set; }

    public string PassportNumber { get; set; }

    public GenderEnum GenderEnum { get; set; }

    public string PhotoUrl { get; set; }
}