using CourierService.Interfaces;
using CourierService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourierService.Offers
{
    public class OFR003Offer : IOffer
    {
        public string OfferCode => "OFR003";

        public double DiscountPercent => 5;

        public bool IsCriteriaSatisfied(Package pkg)
        {
            return pkg.Distance >= 50 &&
              pkg.Distance <= 250 &&
              pkg.Weight >= 10 &&
              pkg.Weight <= 150;
        }
    }
}
