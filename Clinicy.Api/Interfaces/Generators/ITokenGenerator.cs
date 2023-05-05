using System.Security.Claims;

namespace Clinicy.WebApi.Interfaces.Generators;

public interface ITokenGenerator
{
    public string GenerateToken(string secret, string issuer, string audience,
        int expiresInMinutes, ICollection<Claim>? claims = null);
}