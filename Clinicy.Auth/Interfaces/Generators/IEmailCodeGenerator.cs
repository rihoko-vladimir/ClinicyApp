namespace Clinicy.Auth.Interfaces.Generators;

public interface IEmailCodeGenerator
{
    public string GenerateCode();

    public bool VerifyCode(string token, UserCredential userCredential);
}