namespace Clinicy.WebApi.Models.Entities;

public class Cabinet
{
    public Guid Id { get; set; }

    public string Number { get; set; }

    public string Location { get; set; }

    public string DepartmentName { get; set; }

    public string ContactNumber { get; set; }

    public DateTime WorkingHoursFrom { get; set; }

    public DateTime WorkingHoursUntil { get; set; }
}