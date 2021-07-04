using LetsCode.Resistance.Domain;
using System;

namespace LetsCode.Resistance.Infrastructure.RequestModels
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