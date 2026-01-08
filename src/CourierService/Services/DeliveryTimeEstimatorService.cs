using CourierService.Interfaces;
using CourierService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourierService.Services
{
    public class DeliveryTimeEstimatorService
    {
        public DeliveryTimeEstimatorService()
        {
            
        }
        //ToDO  - Time calculation is not as per expectaion
        public List<(string pkgId, double time)> EstimatePackageDeliveryTime(List<Package> packages, List<Vehicle> vehicles)
        {
            var results = new List<(string, double)>();

            //Sort packages by weight DESC
            var remainingPackages = packages.OrderByDescending(k => k.Weight).ToList();

            //Create package delivery shipment for vehicle within weight limit
            while (remainingPackages.Any())
            {
                remainingPackages = remainingPackages
                    .OrderByDescending(p => p.Weight)
                    .ThenBy(p => p.Distance)
                    .ToList();

                var vehicle = vehicles.OrderBy(v => v.NextAvailableTime).First();

                double totalWeight = 0;
                var shipment = new List<Package>();

                foreach (var pkg in remainingPackages)
                {
                    if (totalWeight + pkg.Weight <= vehicle.MaxLoad)
                    {
                        shipment.Add(pkg);
                        totalWeight += pkg.Weight;
                    }
                }

                double maxDistance = shipment.Max(p => p.Distance);
                double travelTime = maxDistance / vehicle.Speed;

                //calculate completion time
                double completionTime = vehicle.NextAvailableTime + travelTime;

                foreach (var pkg in shipment)
                {
                    results.Add((pkg.Id, Math.Round(completionTime, 2)));
                }

                vehicle.NextAvailableTime += 2 * travelTime;

                foreach (var pkg in shipment)
                {
                    remainingPackages.Remove(pkg);
                }
            }

            return results;
        }

    }
}
