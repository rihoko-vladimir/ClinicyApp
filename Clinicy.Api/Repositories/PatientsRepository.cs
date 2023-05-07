using Clinicy.WebApi.Common.SqlCommands;
using Clinicy.WebApi.Interfaces.Factories;
using Clinicy.WebApi.Interfaces.Repositories;
using Clinicy.WebApi.Models;
using Clinicy.WebApi.Models.Entities;
using Dapper;
using Serilog;

namespace Clinicy.WebApi.Repositories;

public class PatientsRepository : IPatientsRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public PatientsRepository(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<Guid> CreatePatient(Patient patient, Guid patientId)
    {
        await using var dbConnection = _dbConnectionFactory.GetConnection();

        Log.Information("Creating new patient with id {Id}", patient.Id);

        var request = PatientSqlCommand.CreatePatientRequest(patient, patientId);

        await dbConnection.ExecuteAsync(request.Query, request.DynamicParameters, commandTimeout: 5000);

        return patient.Id;
    }

    public async Task<Patient?> GetPatientById(Guid patientId)
    {
        await using var dbConnection = _dbConnectionFactory.GetConnection();

        Log.Information("Creating new patient with id {Id}", patientId);

        var request = PatientSqlCommand.GetPatientByIdRequest(patientId);

        var patient =
            await dbConnection.QueryFirstOrDefaultAsync<Patient>(request.Query, request.DynamicParameters,
                commandTimeout: 5000);

        if (patient is not null)
            return patient;

        Log.Warning("Patient with id {Id} wasn't found", patientId);

        return null;
    }

    public async Task<IEnumerable<Patient>> FindPatientsByCriteria(string firstName, string? lastName,
        string? passportNumber,
        string? email, GenderEnum? gender)
    {
        await using var dbConnection = _dbConnectionFactory.GetConnection();

        Log.Information("Finding patients with criterias {FirstName}, {LastName}, {PassportNumber}, {Email}, {Gender}",
            firstName, lastName, passportNumber, email, gender.ToString());

        var request =
            PatientSqlCommand.GetPatientsByCriteriaRequest(firstName, lastName, passportNumber, email,
                gender.ToString());

        var patients =
            await dbConnection.QueryAsync<Patient>(request.Query, request.DynamicParameters, commandTimeout: 5000);

        return patients;
    }

    public async Task<IEnumerable<Patient>> GetAllPatients()
    {
        await using var dbConnection = _dbConnectionFactory.GetConnection();

        Log.Information("Getting all patients");

        var request =
            PatientSqlCommand.GetAllPatientsRequest();

        var patients =
            await dbConnection.QueryAsync<Patient>(request.Query, request.DynamicParameters, commandTimeout: 5000);

        return patients;
    }
}