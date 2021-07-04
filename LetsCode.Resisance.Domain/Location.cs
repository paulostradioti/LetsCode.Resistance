using System;

namespace LetsCode.Resistance.Domain
{
    public class Location
    {
        public Guid Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string BaseName { get; set; }
    }
}