using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourierService.Models
{
    public class ShipmentInputRequest
    {
        public double MaxCarriableWeight { get; set; }
        public double MaximumSpeed { get; set; }
        public int TotalVehiclesCount { get;set; }
        public List<Vehicle> Vehicles { get;set; }
    }
}
