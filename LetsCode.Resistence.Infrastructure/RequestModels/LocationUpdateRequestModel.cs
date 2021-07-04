﻿using System;

namespace LetsCode.Resistance.Infrastructure.RequestModels
{
    public class LocationUpdateRequestModel
    {
        public Guid RebelId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string BaseName { get; set; }
    }
}