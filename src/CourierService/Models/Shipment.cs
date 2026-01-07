using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourierService.Models
{
    public class Shipment
    {
        public List<Package> Packages { get; set; } = new List<Package>();
        public double TotalWeight { get; set; }
        public double MaximumDistance { get; set; }
    }
}
