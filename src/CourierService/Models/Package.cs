using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourierService.Models
{
    public class Package
    {
        public string Id { get; set; }
        public double Weight { get; set; }
        public double Distance { get; set; }
        public string OfferCode { get; set; }
    }
}

