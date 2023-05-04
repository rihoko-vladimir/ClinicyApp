using Clinicy.WebApi.Interfaces.Repositories;
using Clinicy.WebApi.Models;
using Clinicy.WebApi.Models.Entities;

namespace Clinicy.WebApi.Repositories;

public class PatientsRepository : IPatientsRepository
{
    public Task<Guid> CreatePatient(Patient patient)
    {
        throw new NotImplementedException();
    }

    public Task<Patient> GetPatientById(Guid patientId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Patient>> FindPatientsByCriteria(string firstName, string lastName, string passportNumber, string email, GenderEnum gender)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Patient>> GetAllPatients()
    {
        throw new NotImplementedException();
    }
}