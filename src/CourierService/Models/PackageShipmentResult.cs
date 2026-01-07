using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourierService.Models
{
    public class PackageShipmentResult
    {
        public string PackageId { get; set; }
        public double Discount { get; set; }
        public double TotalCost { get; set; }
        public double EstimatedDeliveryTime { get; set; }
    }
}
