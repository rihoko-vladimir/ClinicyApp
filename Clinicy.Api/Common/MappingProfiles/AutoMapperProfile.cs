using AutoMapper;
using Clinicy.WebApi.Models;
using Clinicy.WebApi.Models.Entities;
using Clinicy.WebApi.Models.Responses;
using Shared.Models.Messages;

namespace Clinicy.WebApi.Common.MappingProfiles;

//Автомаппер маппит типы из одного в другие. Полезно для скрытия полей или простого преобразований типов
public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Doctor, DoctorResponse>()
            .ReverseMap();
        CreateMap<Patient, PatientResponse>()
            .ReverseMap();
        CreateMap<Ticket, TicketResponse>()
            .ReverseMap();
        CreateMap<RegisterNewPatientMessage, Patient>()
            .ForMember(patient => patient.GenderEnum,
                expression =>
                    expression.MapFrom(message =>
                        GenderExtensions.ParseCharToGender(message.Gender)))
            .ReverseMap();
    }
}