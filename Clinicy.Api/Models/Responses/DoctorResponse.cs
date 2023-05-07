namespace Clinicy.WebApi.Models.Responses;

public class DoctorResponse
{
    public Guid Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string ParentsName { get; set; }

    public string PhoneNumber { get; set; }

    public string Email { get; set; }

    public string Social { get; set; }

    public string QualificationName { get; set; }

    public string PhotoUrl { get; set; }
}