namespace Clinicy.Auth.Interfaces.Services;

public interface ITokenService
{
    public string GetToken(Guid id);
}