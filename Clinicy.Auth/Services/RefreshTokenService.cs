using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Clinicy.Auth.Interfaces.Generators;
using Clinicy.Auth.Interfaces.Services;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Shared.Models.Models.Configurations;
using ClaimTypes = Shared.Models.Constants.ClaimTypes;

namespace Clinicy.Auth.Services;

public class RefreshTokenService : IRefreshTokenService
{
    private readonly JwtConfiguration _jwtConfiguration;

    private readonly ITokenGenerator _tokenGenerator;

    public RefreshTokenService(ITokenGenerator tokenGenerator, JwtConfiguration configuration)
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

        var token = _tokenGenerator.GenerateToken(_jwtConfiguration.RefreshSecret,
            _jwtConfiguration.Issuer,
            _jwtConfiguration.Audience,
            _jwtConfiguration.RefreshExpiresIn,
            claims);

        Log.Information("Generated JWT Refresh token {Token} for User {UserId}", token, id);

        return token;
    }

    public bool VerifyToken(Guid userId, string refreshToken)
    {
        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguration.RefreshSecret));
        var verificationProperties = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateLifetime = true,
            IssuerSigningKey = signingKey,
            ValidIssuer = _jwtConfiguration.Issuer,
            ValidAudience = _jwtConfiguration.Audience
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            var claimsPrincipal = tokenHandler.ValidateToken(refreshToken, verificationProperties, out _);
            var id = claimsPrincipal.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Id)?.Value;

            return id == userId.ToString();
        }
        catch (Exception)
        {
            Log.Warning("Verification failed: Refresh token is invalid. User: {UserId}, Token: {Token}", userId,
                refreshToken);

            return false;
        }
    }

    public bool GetGuidFromRefreshToken(string refreshToken, out Guid userId)
    {
        try
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(refreshToken);

            var id = token.Claims.First(claim => claim.Type == ClaimTypes.Id).Value;
            var guid = Guid.Parse(id);
            userId = guid;

            return true;
        }
        catch (Exception e)
        {
            Log.Warning("Invalid Refresh token provided. Exception: {Type}, Message: {Message}", e.GetType().FullName,
                e.Message);
            userId = Guid.Empty;

            return false;
        }
    }
}