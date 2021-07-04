using AutoMapper;
using LetsCode.Resistance.Domain;
using LetsCode.Resistance.Infrastructure.RequestModels;

namespace LetsCode.Resistance.Infrastructure.Maps
{
    public class MapProfiles : Profile
    {
        public MapProfiles()
        {
            CreateMap<RebelCreateRequestModel, Rebel>().ReverseMap();
            CreateMap<RebelLocationModel, Location>().ReverseMap();
            CreateMap<RebelUpdateRequestModel, Rebel>().ReverseMap();
            CreateMap<Location, Location>().ForMember(x => x.Id,
                opt => opt.Ignore());
            CreateMap<Rebel, Rebel>();
            CreateMap<LocationUpdateRequestModel, Location>();
        }
    }
}