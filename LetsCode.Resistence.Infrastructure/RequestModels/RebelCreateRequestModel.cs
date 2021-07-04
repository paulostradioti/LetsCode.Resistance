using System.Collections.Generic;
using LetsCode.Resistance.Domain;

namespace LetsCode.Resistance.Infrastructure.RequestModels
{
    public class RebelCreateRequestModel
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public RebelLocationModel Location { get; set; }
        public List<InventoryItemModel> Inventory { get; set; }
    }
}