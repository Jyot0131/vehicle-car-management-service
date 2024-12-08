using Api.Domain.Entities;
using Api.Domain.RequestMessages;
using Api.Domain.ResponseMessages;
using AutoMapper;

namespace Api.MappingProfiles;

public class VehicleMappingProfile : Profile
{
    public VehicleMappingProfile()
    {
        CreateMap<VehicleDetails, GetVehicleDetailResponseMessage>();
        CreateMap<CreateVehicleRequestMessage, VehicleDetails>()
            .ForMember(dest => dest.Active, opt => opt.MapFrom(src => true))
            ;
    }
}