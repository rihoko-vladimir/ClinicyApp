using AutoMapper;
using Clinicy.Auth.Models.Request;

namespace Clinicy.Auth.Common.MappingProfiles;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<RegisterPatientRequest, RegisterPatientRequest>()
            .ReverseMap();
    }
}