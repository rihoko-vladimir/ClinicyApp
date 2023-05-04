namespace Clinicy.Auth.Interfaces.Services;

public interface IResetTokenService : ITokenService
{
    public bool VerifyToken(Guid userId, string resetToken);
}