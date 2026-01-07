using CourierService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourierService.Interfaces
{
    public interface IShipmentScheduler
    {
        List<PackageShipmentResult> ScheduleDeliveries(List<Package> packages, List<Vehicle> vehicles);
    }
    public interface IDeliveryTimeEstimatorService
    {
       
    }
}
