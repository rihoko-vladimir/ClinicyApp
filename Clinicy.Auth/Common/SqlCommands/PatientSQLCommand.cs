using System.Data;
using Clinicy.Auth.Models;
using Clinicy.Auth.Models.Constants;
using Dapper;

namespace Clinicy.Auth.Common.SqlCommands;

public static class PatientSqlCommand
{
    private const string GetPatientCredential = "exec GetPatientCredential @login";
    private const string CreateNewPatient = "exec CreateNewPatient @email, @passwordHash, @emailConfirmationCode";

    public static PreparedRequest CreateNewUserCredentialRequest(PatientCredentials patientCredentials)
    {
        var dynamicParams = new DynamicParameters();

        dynamicParams.Add("email", patientCredentials.PasswordHash, DbType.StringFixedLength);
        dynamicParams.Add("passwordHash", patientCredentials.PasswordHash, DbType.StringFixedLength);
        dynamicParams.Add("emailConfirmationCode", patientCredentials.EmailConfirmationCode, DbType.StringFixedLength);

        return new PreparedRequest(CreateNewPatient, dynamicParams);
    }

    public static PreparedRequest GetPatientCredentialRequest(string login)
    {
        var dynamicParams = new DynamicParameters();

        dynamicParams.Add("login", login, DbType.StringFixedLength);

        return new PreparedRequest(GetPatientCredential, dynamicParams);
    }
}