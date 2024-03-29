﻿using System;
using System.Collections.Generic;

namespace LetsCode.Resistance.Domain
{
    public class Rebel
    {
        private const int TreasonCount = 3;

        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public int ReportCount { get; set; }
        public Location Location { get; set; }
        public List<InventoryItem> Inventory { get; set; }
        public bool IsTraitor { get; set; }

        public void Report()
        {
            ReportCount++;
            IsTraitor = ReportCount >= TreasonCount;
        }
    }
}