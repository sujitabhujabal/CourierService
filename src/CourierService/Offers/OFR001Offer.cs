using CourierService.Interfaces;
using CourierService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourierService.Offers
{
    public class OFR001Offer : IOffer
    {
        public string OfferCode => "OFR001";

        public double DiscountPercent => 10;

        public bool IsCriteriaSatisfied(Package pkg)
        {
            return pkg.Distance >= 70 &&
                pkg.Distance <= 200 &&
                pkg.Weight >= 10 &&
                pkg.Weight <= 150;
        }
    }
}
