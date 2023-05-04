using Clinicy.WebApi.Interfaces.Repositories;
using Clinicy.WebApi.Models;
using Clinicy.WebApi.Models.Entities;

namespace Clinicy.WebApi.Repositories;

public class DoctorsRepository : IDoctorsRepository
{
    public Task<Doctor> GetDoctorById(Guid doctorId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Doctor>> FindDoctorsByCriteria(string firstName, string lastName, string parentsName,
        string qualification)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Doctor>> GetAllDoctors()
    {
        throw new NotImplementedException();
    }
}