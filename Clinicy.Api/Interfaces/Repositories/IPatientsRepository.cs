using Clinicy.WebApi.Models;

namespace Clinicy.WebApi.Interfaces.Repositories;

public interface IPatientsRepository
{
    public Task<Guid> CreatePatient(Patient patient);
    
    public Task<Patient> GetPatientById(Guid patientId);

    public Task<IEnumerable<Patient>> FindDoctorsByCriteria(string firstName, string lastName, string passportNumber,
        string email, GenderEnum gender);

    public Task<IEnumerable<Patient>> GetAllDoctors();
}