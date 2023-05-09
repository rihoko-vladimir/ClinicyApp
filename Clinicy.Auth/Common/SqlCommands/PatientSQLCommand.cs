using System.Data;
using Clinicy.Auth.Models;
using Clinicy.Auth.Models.Constants;
using Dapper;

namespace Clinicy.Auth.Common.SqlCommands;

public static class PatientSqlCommand
{
    private const string GetPatientCredential = "exec GetPatientCredential @login";
    private const string CreateNewPatient = "exec CreateNewPatient @email, @passwordHash, @emailConfirmationCode";
    private const string GetPatientCredentialByGuid = "exec GetPatientCredentialByGuid @guid";

    public static PreparedRequest CreateNewUserCredentialRequest(AccountCredentials accountCredentials)
    {
        var dynamicParams = new DynamicParameters();

        dynamicParams.Add("email", accountCredentials.Email, DbType.StringFixedLength);
        dynamicParams.Add("passwordHash", accountCredentials.PasswordHash, DbType.StringFixedLength);
        dynamicParams.Add("emailConfirmationCode", accountCredentials.EmailConfirmationCode, DbType.StringFixedLength);
        dynamicParams.Add("role", accountCredentials.AccountRole, DbType.StringFixedLength);

        return new PreparedRequest(CreateNewPatient, dynamicParams);
    }

    public static PreparedRequest GetPatientCredentialRequest(string login)
    {
        var dynamicParams = new DynamicParameters();

        dynamicParams.Add("login", login, DbType.StringFixedLength);

        return new PreparedRequest(GetPatientCredential, dynamicParams);
    }

    public static PreparedRequest GetPatientCredentialByGuidRequest(Guid guid)
    {
        var dynamicParams = new DynamicParameters();

        dynamicParams.Add("guid", guid, DbType.Guid);

        return new PreparedRequest(GetPatientCredentialByGuid, dynamicParams);
    }
}