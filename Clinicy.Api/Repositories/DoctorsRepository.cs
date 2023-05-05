using Clinicy.WebApi.Common.SqlCommands;
using Clinicy.WebApi.Interfaces.Factories;
using Clinicy.WebApi.Interfaces.Repositories;
using Clinicy.WebApi.Models.Entities;
using Dapper;
using Serilog;

namespace Clinicy.WebApi.Repositories;

public class DoctorsRepository : IDoctorsRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public DoctorsRepository(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<Doctor?> GetDoctorById(Guid doctorId)
    {
        await using var dbConnection = _dbConnectionFactory.GetConnection();

        var request = DoctorSqlCommand.GetDoctorByIdRequest(doctorId);

        Log.Information("Querying for doctor with Id {Id}", doctorId);

        var doctor =
            await dbConnection.QueryFirstOrDefaultAsync<Doctor>(request.Query, request.DynamicParameters,
                commandTimeout: 5000);

        if (doctor is not null)
            return doctor;

        Log.Warning("Doctor with id {Id} wasn't found", doctorId);

        return null;
    }

    public async Task<IEnumerable<Doctor>> FindDoctorsByCriteria(string firstName, string? lastName,
        string? parentsName,
        string? qualification)
    {
        await using var dbConnection = _dbConnectionFactory.GetConnection();

        var request = DoctorSqlCommand.GetDoctorsByCriteriaRequest(firstName, lastName, parentsName, qualification);

        Log.Information(
            "Querying for doctors with the next criterias {FirstName}, {LastName}, {ParentsName}, {Qualification}",
            firstName, lastName, parentsName, qualification);

        var doctors =
            await dbConnection.QueryAsync<Doctor>(request.Query, request.DynamicParameters,
                commandTimeout: 5000);

        return doctors;
    }

    public async Task<IEnumerable<Doctor>> GetAllDoctors()
    {
        await using var dbConnection = _dbConnectionFactory.GetConnection();

        var request = DoctorSqlCommand.GetAllDoctorsRequest();

        Log.Information("Querying for all doctors available");

        var doctors = await dbConnection.QueryAsync<Doctor>(request.Query, request.DynamicParameters,
            commandTimeout: 5000);

        return doctors;
    }
}