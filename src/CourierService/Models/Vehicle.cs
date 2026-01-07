using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourierService.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public double Speed { get; set; }
        public double MaxLoad { get; set; }
        public double NextAvailableTime { get; set; } = 0;
    }
}
