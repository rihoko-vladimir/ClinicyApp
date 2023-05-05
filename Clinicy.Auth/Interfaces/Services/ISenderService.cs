using MassTransit;

namespace Clinicy.Auth.Interfaces.Services;

public interface ISenderService
{
    public Task<ValidationResultExtensions.Result> SendEmailCodeMessageAsync(string emailCode, string emailAddress,
        string firstName);

    public Task<ValidationResultExtensions.Result> SendResetPasswordMessageAsync(string resetToken, string emailAddress,
        string firstName);

    public Task<ValidationResultExtensions.Result> SendRegistrationMessageAsync(Guid userId, string firstName,
        string lastName,
        string userName, bool isTermsAccepted);
}