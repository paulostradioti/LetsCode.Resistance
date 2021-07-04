using System;

namespace LetsCode.Resistance.Domain
{
    public class Price
    {
        public Guid Id { get; set; }
        public string ItemName { get; set; }
        public int PriceInPoints { get; set; }
    }
}