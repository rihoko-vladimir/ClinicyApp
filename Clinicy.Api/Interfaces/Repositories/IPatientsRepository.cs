using Clinicy.WebApi.Models;
using Clinicy.WebApi.Models.Entities;

namespace Clinicy.WebApi.Interfaces.Repositories;

public interface IPatientsRepository
{
    public Task<Guid> CreatePatient(Patient patient);

    public Task<Patient> GetPatientById(Guid patientId);

    public Task<IEnumerable<Patient>> FindPatientsByCriteria(string firstName, string lastName, string passportNumber,
        string email, GenderEnum gender);

    public Task<IEnumerable<Patient>> GetAllPatients();
}