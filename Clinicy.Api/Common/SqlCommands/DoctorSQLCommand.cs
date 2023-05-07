using System.Data;
using Clinicy.WebApi.Models.Constants;
using Dapper;

namespace Clinicy.WebApi.Common.SqlCommands;

public static class DoctorSqlCommand
{
    private const string GetAllDoctors = "exec GetAllDoctors";
    private const string GetDoctorById = "exec GetDoctorById @doctorId";

    private const string GetDoctorByCriteria =
        "exec GetDoctorsByCriteria @firstName, @lastName, @parentsName, @qualification";

    public static PreparedRequest GetAllDoctorsRequest()
    {
        var dynamicParams = new DynamicParameters();

        return new PreparedRequest(GetAllDoctors, dynamicParams);
    }

    public static PreparedRequest GetDoctorByIdRequest(Guid doctorId)
    {
        var dynamicParams = new DynamicParameters();

        dynamicParams.Add("doctorId", doctorId, DbType.Guid);

        return new PreparedRequest(GetDoctorById, dynamicParams);
    }

    public static PreparedRequest GetDoctorsByCriteriaRequest(string firstName, string? lastName, string? parentsName,
        string? qualification)
    {
        var dynamicParams = new DynamicParameters();

        dynamicParams.Add("firstName", firstName, DbType.StringFixedLength);
        dynamicParams.Add("lastName", lastName, DbType.StringFixedLength);
        dynamicParams.Add("parentsName", parentsName, DbType.StringFixedLength);
        dynamicParams.Add("qualification", qualification, DbType.StringFixedLength);

        return new PreparedRequest(GetDoctorByCriteria, dynamicParams);
    }
}