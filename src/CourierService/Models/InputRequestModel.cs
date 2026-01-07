using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourierService.Models
{
    public class InputRequestModel
    {
        public double BaseDeliveryCost { get; set; }
        public double NoOfPackages { get; set; }

        public List<Package> Packages { get; set; }
    }
}
