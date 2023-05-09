namespace Clinicy.Auth.Interfaces.Services;

public interface IAccessTokenService
{
    public string GetToken(Guid id, string role);

    public bool GetGuidFromAccessToken(string accessToken, out Guid userId);
}