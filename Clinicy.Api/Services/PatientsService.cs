using Clinicy.WebApi.Interfaces.Repositories;
using Clinicy.WebApi.Interfaces.Services;
using Clinicy.WebApi.Models;
using Clinicy.WebApi.Models.Entities;

namespace Clinicy.WebApi.Services;

public class PatientsService : IPatientsService
{
    private readonly IPatientsRepository _patientsRepository;

    public PatientsService(IPatientsRepository patientsRepository)
    {
        _patientsRepository = patientsRepository;
    }

    public async Task<Guid> CreatePatient(Patient patient, Guid patientId)
    {
        return await _patientsRepository.CreatePatient(patient, patientId);
    }

    public async Task<Patient?> GetPatientById(Guid patientId)
    {
        return await _patientsRepository.GetPatientById(patientId);
    }

    public async Task<IEnumerable<Patient>> FindPatientsByCriteria(string firstName, string? lastName,
        string? passportNumber,
        string? email,
        GenderEnum? gender)
    {
        return await _patientsRepository.FindPatientsByCriteria(firstName, lastName, passportNumber, email, gender);
    }

    public async Task<IEnumerable<Patient>> GetAllPatients()
    {
        return await _patientsRepository.GetAllPatients();
    }
}