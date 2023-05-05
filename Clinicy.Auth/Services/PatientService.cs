using Clinicy.Auth.Interfaces.Services;
using Clinicy.Auth.Models.Request;

namespace Clinicy.Auth.Services;

public class PatientService : IPatientService
{
    public Task CreatePatientAsync(RegisterPatientRequest registerPatientRequest)
    {
        throw new NotImplementedException();
    }

    public Task<bool> LoginPatientAsync()
    {
        throw new NotImplementedException();
    }
}