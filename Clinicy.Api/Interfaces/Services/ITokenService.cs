namespace Clinicy.WebApi.Interfaces.Services;

public interface ITokenService
{
    public string GetToken(Guid id);
}