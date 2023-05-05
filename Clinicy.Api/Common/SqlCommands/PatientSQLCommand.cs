using System.Data;
using Clinicy.WebApi.Models.Constants;
using Clinicy.WebApi.Models.Entities;
using Dapper;

namespace Clinicy.WebApi.Common.SqlCommands;

public static class PatientSqlCommand
{
    private const string GetAllPatients = "exec GetAllPatients";
    private const string GetPatientById = "exec GetPatientById @patientId";

    private const string GetPatientsByCriteria =
        "exec GetPatientsByCriteria @firstName, @lastName, @passportNumber, @email, @gender";

    private const string CreatePatient =
        "exec CreatePatient @patientId, @firstName, @lastName, @contactNumber, @email, @passportNumber, @gender, @photoUrl";

    public static PreparedRequest GetAllPatientsRequest()
    {
        var dynamicParams = new DynamicParameters();

        return new PreparedRequest(GetAllPatients, dynamicParams);
    }

    public static PreparedRequest GetPatientByIdRequest(Guid patientId)
    {
        var dynamicParams = new DynamicParameters();

        dynamicParams.Add("patientId", patientId, DbType.Guid);

        return new PreparedRequest(GetPatientById, dynamicParams);
    }

    public static PreparedRequest GetPatientsByCriteriaRequest(string firstName, string? lastName,
        string? passportNumber,
        string? email, string? gender)
    {
        var dynamicParams = new DynamicParameters();

        dynamicParams.Add("firstName", firstName, DbType.StringFixedLength);
        dynamicParams.Add("lastName", lastName, DbType.StringFixedLength);
        dynamicParams.Add("passportNumber", passportNumber, DbType.StringFixedLength);
        dynamicParams.Add("email", email, DbType.StringFixedLength);
        dynamicParams.Add("gender", gender, DbType.StringFixedLength);

        return new PreparedRequest(GetPatientsByCriteria, dynamicParams);
    }

    public static PreparedRequest CreatePatientRequest(Patient patient, Guid patientId)
    {
        var dynamicParams = new DynamicParameters();
        //TODO finish patient creation
        dynamicParams.Add("firstName", patient.FirstName, DbType.StringFixedLength);
        dynamicParams.Add("lastName", patient.LastName, DbType.StringFixedLength);
        dynamicParams.Add("contactNumber", patient.ContactNumber, DbType.StringFixedLength);
        dynamicParams.Add("email", patient.Email, DbType.StringFixedLength);
        dynamicParams.Add("passportNumber", patient.PassportNumber, DbType.StringFixedLength);
        dynamicParams.Add("gender", patient.GenderEnum.ToString(), DbType.StringFixedLength);
        dynamicParams.Add("photoUrl", patient.PhotoUrl, DbType.StringFixedLength);

        return new PreparedRequest(CreatePatient, dynamicParams);
    }
}