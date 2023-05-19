using System.IdentityModel.Tokens.Jwt;
using Clinicy.WebApi.Interfaces.Services;
using Serilog;
using Shared.Models.Constants;

namespace Clinicy.WebApi.Services;

public class AccessTokenService : IAccessTokenService
{
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

    public string GetToken(Guid id)
    {
        return "";
    }
    
    public bool GetRoleFromAccessToken(string accessToken, out string role)
    {
        try
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(accessToken);
            var roleL = token.Claims.First(claim => claim.Type == ClaimTypes.Role).Value;
            role = roleL;

            return true;
        }
        catch (Exception e)
        {
            Log.Warning("Invalid Access token provided. Exception: {Type}, Message: {Message}", e.GetType().FullName,
                e.Message);
            role = string.Empty;

            return false;
        }
    }
}