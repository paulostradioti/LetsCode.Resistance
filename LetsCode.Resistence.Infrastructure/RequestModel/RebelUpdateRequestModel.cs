using System;
using LetsCode.Resistance.Domain;

namespace LetsCode.Resistance.Infrastructure.RequestModel
{
    public class RebelUpdateRequestModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public RebelLocationModel Location { get; set; }
    }
}