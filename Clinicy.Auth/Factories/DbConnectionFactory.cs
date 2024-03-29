using System.Data.Common;
using Clinicy.Auth.Interfaces.Factories;
using Microsoft.Data.SqlClient;

namespace Clinicy.Auth.Factories;

public class DbConnectionFactory : IDbConnectionFactory
{
    private readonly string _connectionString;

    public DbConnectionFactory(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("ClinicDbConnectionString") ??
                            throw new InvalidOperationException();
    }

    public DbConnection GetConnection()
    {
        var connection = new SqlConnection(_connectionString);

        return connection;
    }
}