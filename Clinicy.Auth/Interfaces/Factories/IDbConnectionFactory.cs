using System.Data.Common;

namespace Clinicy.Auth.Interfaces.Factories;

public interface IDbConnectionFactory
{
    public DbConnection GetConnection();
}