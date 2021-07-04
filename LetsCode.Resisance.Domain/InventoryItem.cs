using System;

namespace LetsCode.Resistance.Domain
{
    public class InventoryItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public Guid RebelId { get; set; }
    }
}