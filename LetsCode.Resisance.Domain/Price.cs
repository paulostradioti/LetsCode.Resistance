using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsCode.Resistance.Domain
{
    public class Price
    {
        public Guid Id { get; set; }
        public string ItemName { get; set; }
        public int PriceInPoints { get; set; }
    }
}
