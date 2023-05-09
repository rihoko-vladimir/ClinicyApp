using AutoMapper;
using Clinicy.WebApi.Interfaces.Services;
using Clinicy.WebApi.Models.Entities;
using MassTransit;
using Serilog;
using Shared.Models.Messages;

namespace Clinicy.WebApi.Consumers;

//Данный класс получает сообщения из брокера и выполняет некую логику
public class RegisterNewPatientConsumer : IConsumer<RegisterNewPatientMessage>
{
    private readonly IMapper _mapper;
    private readonly IPatientsService _patientsService;

    public RegisterNewPatientConsumer(IMapper mapper, IPatientsService patientsService)
    {
        _mapper = mapper;
        _patientsService = patientsService;
    }

    public async Task Consume(ConsumeContext<RegisterNewPatientMessage> context)
    {
        var mappedPatient = _mapper.Map<Patient>(context.Message);

        var guid = await _patientsService.CreatePatient(mappedPatient, context.Message.Id);

        Log.Information("Created new patient with id {Id}", guid);
    }
}