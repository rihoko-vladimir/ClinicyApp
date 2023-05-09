using Clinicy.WebApi.Interfaces.Services;
using MassTransit;
using Serilog;
using Shared.Models.Messages;
using Shared.Models.QueueNames;

namespace Clinicy.WebApi.Services;

public class SenderService : ISenderService
{
    private readonly ISendEndpointProvider _sendEndpointProvider;

    public SenderService(ISendEndpointProvider sendEndpointProvider)
    {
        _sendEndpointProvider = sendEndpointProvider;
    }

    public async Task SendExportMessageAsync()
    {
        Log.Information("Exporting at {DateTime}", DateTime.Now);

        await SendMessage(new Uri($"queue:{QueueNames.Export}"), new ExportMessage());
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