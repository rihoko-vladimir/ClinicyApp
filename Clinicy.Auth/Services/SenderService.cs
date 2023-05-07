using AutoMapper;
using Clinicy.Auth.Interfaces.Services;
using Clinicy.Auth.Models.Request;
using MassTransit;
using Serilog;
using Shared.Models.Messages;
using Shared.Models.QueueNames;

namespace Clinicy.Auth.Services;

public class SenderService : ISenderService
{
    private readonly IMapper _mapper;
    private readonly ISendEndpointProvider _sendEndpointProvider;

    public SenderService(ISendEndpointProvider sendEndpointProvider, IMapper mapper)
    {
        _sendEndpointProvider = sendEndpointProvider;
        _mapper = mapper;
    }

    public async Task SendRegistrationRequestAsync(RegisterPatientRequest patientRequest, Guid credentialId)
    {
        Log.Information("Registering new user with email {Email}", patientRequest.Email);

        var mapped = _mapper.Map<RegisterNewPatientMessage>(patientRequest);

        mapped.Id = credentialId;

        await SendMessage(new Uri($"queue:{QueueNames.RegisterUser}"), mapped);
    }

    private async Task SendMessage<T>(Uri endpointUri, T message)
    {
        try
        {
            var endpoint =
                await _sendEndpointProvider.GetSendEndpoint(endpointUri);

            Log.Information("Sending to {Uri}", endpointUri.ToString());

            await endpoint.Send(message!);
        }
        catch (Exception e)
        {
            Log.Error(
                "An error occured while attempting to send a message. Exception: {Type}, Message: {Message}",
                e.GetType().FullName, e.Message);
        }
    }
}