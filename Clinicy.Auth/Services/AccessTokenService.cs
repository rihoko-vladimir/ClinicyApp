using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Clinicy.Auth.Interfaces.Generators;
using Clinicy.Auth.Interfaces.Services;
using Serilog;
using Shared.Models.Models.Configurations;
using ClaimTypes = Shared.Models.Constants.ClaimTypes;

namespace Clinicy.Auth.Services;

public class AccessTokenService : IAccessTokenService
{
    private readonly JwtConfiguration _jwtConfiguration;
    private readonly ITokenGenerator _tokenGenerator;

    public AccessTokenService(ITokenGenerator tokenGenerator, JwtConfiguration configuration)
    {
        _tokenGenerator = tokenGenerator;
        _jwtConfiguration = configuration;
    }

    public string GetToken(Guid id)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Id, id.ToString())
        };

        var token = _tokenGenerator.GenerateToken(_jwtConfiguration.AccessSecret,
            _jwtConfiguration.Issuer,
            _jwtConfiguration.Audience,
            _jwtConfiguration.AccessExpiresIn,
            claims);

        Log.Information("Generated JWT Access token {Token} for User {UserId}", token, id);

        return token;
    }

    public bool GetGuidFromAccessToken(string accessToken, out Guid userId)
    {
        try
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(accessToken);
            var id = token.Claims.First(claim => claim.Type == ClaimTypes.Id).Value;
            var guid = Guid.Parse(id);
            userId = guid;

            return true;
        }
        catch (Exception e)
        {
            Log.Warning("Invalid Access token provided. Exception: {Type}, Message: {Message}", e.GetType().FullName,
                e.Message);
            userId = Guid.Empty;

            return false;
        }
    }
}