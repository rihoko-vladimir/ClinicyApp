using Clinicy.Auth.Common.SqlCommands;
using Clinicy.Auth.Interfaces.Factories;
using Clinicy.Auth.Interfaces.Repositories;
using Clinicy.Auth.Models;
using Clinicy.Auth.Models.Request;
using Dapper;
using Shared.Models.Constants;

namespace Clinicy.Auth.Repositories;

public class PatientRepository : IPatientRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public PatientRepository(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<Guid> CreatePatientAsync(RegisterPatientRequest registerPatientRequest, string passwordHash)
    {
        await using var dbConnection = _dbConnectionFactory.GetConnection();

        var patientCredential = new AccountCredentials
        {
            Email = registerPatientRequest.Email,
            IsEmailConfirmed = true,
            PasswordHash = passwordHash,
            AccountRole = RoleTypes.Patient
        };

        var request = PatientSqlCommand.CreateNewUserCredentialRequest(patientCredential);

        var guid = await dbConnection.QueryFirstAsync<Guid>(request.Query, request.DynamicParameters,
            commandTimeout: 5000);

        return guid;
    }

    public async Task<AccountCredentials?> GetPatientCredentialsAsync(string login)
    {
        await using var dbConnection = _dbConnectionFactory.GetConnection();

        var request = PatientSqlCommand.GetPatientCredentialRequest(login);

        var cred = await dbConnection.QueryFirstOrDefaultAsync<AccountCredentials>(request.Query,
            request.DynamicParameters, commandTimeout: 5000);

        return cred;
    }

    public async Task<AccountCredentials?> GetPatientCredentialsByGuidAsync(Guid guid)
    {
        await using var dbConnection = _dbConnectionFactory.GetConnection();

        var request = PatientSqlCommand.GetPatientCredentialByGuidRequest(guid);

        var cred = await dbConnection.QueryFirstOrDefaultAsync<AccountCredentials>(request.Query,
            request.DynamicParameters, commandTimeout: 5000);

        return cred;
    }
}