using CourierService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourierService.Interfaces
{
    public interface IPlanShipment
    {
        List<Shipment> PlanShipments(List<Package> packages, Vehicle vehicle);
    }
}
