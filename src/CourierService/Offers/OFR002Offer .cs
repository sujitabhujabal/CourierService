using CourierService.Interfaces;
using CourierService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourierService.Offers
{
    public class OFR002Offer : IOffer
    {
        public string OfferCode => "OFR002";

        public double DiscountPercent => 7;

        public bool IsCriteriaSatisfied(Package pkg)
        {
            return pkg.Distance >= 50 &&
             pkg.Distance <= 150 &&
             pkg.Weight >= 100 &&
             pkg.Weight <= 250;
        }
    }
}
