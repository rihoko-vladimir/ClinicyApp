using AutoMapper;
using Clinicy.Auth.Models.Request;
using Shared.Models.Messages;

namespace Clinicy.Auth.Common.MappingProfiles;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<RegisterPatientRequest, RegisterPatientRequest>()
            .ReverseMap();

        CreateMap<RegisterPatientRequest, RegisterNewPatientMessage>()
            .ReverseMap();
    }
}