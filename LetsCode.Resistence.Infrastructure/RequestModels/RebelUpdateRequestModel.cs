﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsCode.Resistance.Infrastructure.RequestModels
{
    public class RebelUpdateRequestModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public RebelLocationModel Location { get; set; }
    }
}