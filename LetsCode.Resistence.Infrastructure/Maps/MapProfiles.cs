using AutoMapper;
using LetsCode.Resistance.Domain;
using LetsCode.Resistance.Infrastructure.RequestModels;

namespace LetsCode.Resistance.Infrastructure.Maps
{
    public class MapProfiles : Profile
    {
        public MapProfiles()
        {
            CreateMap<RebelCreateRequestModel, Rebel>().AfterMap((model, rebel) =>
            {
                rebel.Location = new Location
                {
                    BaseName = model.BaseName,
                    Latitude = model.Latitude,
                    Longitude = model.Longitude
                };
            });

            CreateMap<LocationUpdateRequestModel, Location>();
        }
    }
}