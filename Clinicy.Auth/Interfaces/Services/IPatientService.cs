using Clinicy.Auth.Models.Request;
using Clinicy.Auth.Models.Responses;

namespace Clinicy.Auth.Interfaces.Services;

public interface IPatientService
{
    public Task CreatePatientAsync(RegisterPatientRequest registerPatientRequest);

    public Task<TokensResponse?> LoginPatientAsync(string login, string password);

    public Task<TokensResponse?> RefreshTokensAsync(Guid userId);
}