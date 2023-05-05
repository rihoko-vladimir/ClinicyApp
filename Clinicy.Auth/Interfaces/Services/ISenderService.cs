using Clinicy.Auth.Models.Request;

namespace Clinicy.Auth.Interfaces.Services;

public interface ISenderService
{
    public Task SendRegistrationRequestAsync(RegisterPatientRequest patientRequest);
}