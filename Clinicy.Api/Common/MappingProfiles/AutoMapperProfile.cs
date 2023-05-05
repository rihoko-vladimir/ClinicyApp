using AutoMapper;
using Clinicy.WebApi.Models.Entities;
using Clinicy.WebApi.Models.Responses;

namespace Clinicy.WebApi.Common.MappingProfiles;

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
    }
}