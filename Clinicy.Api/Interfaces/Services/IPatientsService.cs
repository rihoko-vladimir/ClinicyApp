using Clinicy.WebApi.Models;
using Clinicy.WebApi.Models.Entities;

namespace Clinicy.WebApi.Interfaces.Services;

public interface IPatientsService
{
    public Task<Guid> CreatePatient(Patient patient, Guid patientId);

    public Task<Patient?> GetPatientById(Guid patientId);

    public Task<IEnumerable<Patient>> FindPatientsByCriteria(string firstName, string? lastName, string? passportNumber,
        string? email, GenderEnum? gender);

    public Task<IEnumerable<Patient>> GetAllPatients();
}