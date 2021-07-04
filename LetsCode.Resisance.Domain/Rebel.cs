using System;

namespace LetsCode.Resistance.Domain
{
    public class Rebel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public Location Location { get; set; }
    }
}