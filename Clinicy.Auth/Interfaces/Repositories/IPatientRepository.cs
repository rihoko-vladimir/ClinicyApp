using Clinicy.Auth.Models;
using Clinicy.Auth.Models.Request;

namespace Clinicy.Auth.Interfaces.Repositories;

public interface IPatientRepository
{
    public Task<Guid> CreatePatientAsync(RegisterPatientRequest registerPatientRequest, string passwordHash);

    public Task<AccountCredentials?> GetPatientCredentialsAsync(string login);

    public Task<AccountCredentials?> GetPatientCredentialsByGuidAsync(Guid guid);
}