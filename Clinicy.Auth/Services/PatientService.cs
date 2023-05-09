using Clinicy.Auth.Interfaces.Repositories;
using Clinicy.Auth.Interfaces.Services;
using Clinicy.Auth.Models.Request;
using Clinicy.Auth.Models.Responses;
using Serilog;

namespace Clinicy.Auth.Services;

public class PatientService : IPatientService
{
    private readonly IAccessTokenService _accessTokenService;
    private readonly IPatientRepository _patientRepository;
    private readonly IRefreshTokenService _refreshTokenService;
    private readonly ISenderService _service;

    public PatientService(IPatientRepository patientRepository, ISenderService service,
        IAccessTokenService accessTokenService, IRefreshTokenService refreshTokenService)
    {
        _patientRepository = patientRepository;
        _service = service;
        _accessTokenService = accessTokenService;
        _refreshTokenService = refreshTokenService;
    }

    public async Task CreatePatientAsync(RegisterPatientRequest registerPatientRequest)
    {
        var credential = await _patientRepository.GetPatientCredentialsAsync(registerPatientRequest.Email);

        if (credential is not null) return;

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerPatientRequest.Password);

        var patientId = await _patientRepository.CreatePatientAsync(registerPatientRequest, hashedPassword);

        await _service.SendRegistrationRequestAsync(registerPatientRequest, patientId);
    }

    public async Task<TokensResponse?> LoginPatientAsync(string login, string password)
    {
        var credential = await _patientRepository.GetPatientCredentialsAsync(login);

        if (credential is null)
        {
            Log.Warning("User with login {Login} wasn't found", login);

            return null;
        }

        var isPasswordCorrect = BCrypt.Net.BCrypt.Verify(password, credential.PasswordHash);

        if (isPasswordCorrect) return await RefreshTokensAsync(credential.Id);

        Log.Warning("Incorrect password provided for {Login}", login);

        Log.Error("pass {P}", credential.PasswordHash);

        Log.Error("pass1 {P}", password);

        return null;
    }

    public async Task<TokensResponse?> RefreshTokensAsync(Guid userId)
    {
        var credential = await _patientRepository.GetPatientCredentialsByGuidAsync(userId);

        if (credential is null)
        {
            Log.Warning("User with id {UserId} wasn't found", userId);

            return null;
        }

        var access = _accessTokenService.GetToken(credential.Id, credential.AccountRole);
        var refresh = _refreshTokenService.GetToken(credential.Id);

        return new TokensResponse(access, refresh);
    }
}