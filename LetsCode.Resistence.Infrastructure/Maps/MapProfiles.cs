using AutoMapper;
using LetsCode.Resistance.Domain;
using LetsCode.Resistance.Infrastructure.RequestModels;
using System.Linq;

namespace LetsCode.Resistance.Infrastructure.Maps
{
    public class MapProfiles : Profile
    {
        public MapProfiles()
        {
            CreateMap<RebelCreateRequestModel, Rebel>().ForMember(destination => destination.Inventory,
                options =>
                {
                    options.MapFrom(function => function.Inventory.GroupBy(x => x.Name).Select(g => new InventoryItem
                    { Name = g.Key, Quantity = g.Sum(x => x.Quantity) }));
                });

            CreateMap<RebelLocationModel, Location>();
            CreateMap<RebelUpdateRequestModel, Rebel>().ForMember(destinationMember => destinationMember.Inventory,
                memberOptions => memberOptions.Ignore());
            CreateMap<Rebel, Rebel>()
                .ForMember(destinationMember => destinationMember.Inventory, options => options.Ignore());
            CreateMap<InventoryItem, InventoryItem>();
            CreateMap<LocationUpdateRequestModel, Location>();
            CreateMap<InventoryItemModel, InventoryItem>();
        }
    }
}