using System.Data.Common;

namespace Clinicy.WebApi.Interfaces.Factories;

public interface IDbConnectionFactory
{
    public DbConnection GetConnection();
}