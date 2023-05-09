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

        await dbConnection.ExecuteAsync("exec BackupEverything");
    }
}