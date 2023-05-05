using Clinicy.Auth.Models.Request;

namespace Clinicy.Auth.Interfaces.Services;

public interface IPatientService
{
    public Task CreatePatientAsync(RegisterPatientRequest registerPatientRequest);

    public Task<bool> LoginPatientAsync();
}