using Clinicy.WebApi.Interfaces.Repositories;
using Clinicy.WebApi.Interfaces.Services;
using Clinicy.WebApi.Models.Entities;

namespace Clinicy.WebApi.Services;

public class DoctorsService : IDoctorsService
{
    private readonly IDoctorsRepository _doctorsRepository;

    public DoctorsService(IDoctorsRepository doctorsRepository)
    {
        _doctorsRepository = doctorsRepository;
    }

    public async Task<Doctor?> GetDoctorById(Guid doctorId)
    {
        return await _doctorsRepository.GetDoctorById(doctorId);
    }

    public async Task<IEnumerable<Doctor>> FindDoctorsByCriteria(string firstName, string? lastName,
        string? parentsName, string? qualification)
    {
        return await _doctorsRepository.FindDoctorsByCriteria(firstName, lastName, parentsName, qualification);
    }

    public async Task<IEnumerable<Doctor>> GetAllDoctors()
    {
        return await _doctorsRepository.GetAllDoctors();
    }
}