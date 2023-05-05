using Clinicy.WebApi.Models.Entities;

namespace Clinicy.WebApi.Interfaces.Repositories;

public interface IDoctorsRepository
{
    public Task<Doctor?> GetDoctorById(Guid doctorId);

    public Task<IEnumerable<Doctor>> FindDoctorsByCriteria(string firstName, string? lastName, string? parentsName,
        string? qualification);

    public Task<IEnumerable<Doctor>> GetAllDoctors();
}