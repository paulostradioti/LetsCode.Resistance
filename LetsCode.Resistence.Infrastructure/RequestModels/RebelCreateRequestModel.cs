﻿namespace LetsCode.Resistance.Infrastructure.RequestModels
{
    public class RebelCreateRequestModel
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public RebelLocationModel Location { get; set; }
    }
}
