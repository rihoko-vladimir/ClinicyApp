using System.Xml.Serialization;
using Clinicy.WebApi.Models.Entities;
using Dapper;
using MassTransit;
using Microsoft.Data.SqlClient;
using Shared.Models.Messages;

namespace Clinicy.ImportExport.Consumers;

public class ExportConsumer : IConsumer<ExportMessage>
{
    private readonly IConfiguration _configuration;

    public ExportConsumer(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task Consume(ConsumeContext<ExportMessage> context)
    {
        await using var dbConnection =
            new SqlConnection(_configuration.GetConnectionString("ClinicDbConnectionString"));

            var allDoctors = await dbConnection.QueryAsync<Doctor>("exec GetAllDoctors");
            var allPatients = await dbConnection.QueryAsync<Patient>("exec GetAllPatients");

            var patientsSerializer = new XmlSerializer(typeof(Patient));
            var doctorsSerializer = new XmlSerializer(typeof(Doctor));

            await using var patientsFileStream =
                new FileStream($"backups/patients-{DateTime.Now}.xml", FileMode.OpenOrCreate);
            await using var doctorsFileStream =
                new FileStream($"backups/doctors-{DateTime.Now}.xml", FileMode.OpenOrCreate);
            
            patientsSerializer.Serialize(patientsFileStream, allPatients);
            doctorsSerializer.Serialize(doctorsFileStream, allDoctors);
    }
}