using CourierService.Interfaces;
using CourierService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourierService
{
    public class Offer : IOffer
    {
        public string OfferCode { get; set; }

        public double DiscountPercent { get; set; }
        private int MinDistance, MaxDistance, MinWeight, MaxWeight;
        public bool IsCriteriaSatisfied(Package pkg)
        {
            return pkg.Distance >= MinDistance &&
                   pkg.Distance <= MaxDistance &&
                   pkg.Weight >= MinWeight &&
                   pkg.Weight <= MaxWeight;
        }
    }
}
