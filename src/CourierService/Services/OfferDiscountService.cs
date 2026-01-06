using CourierService.Interfaces;
using CourierService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourierService.Services
{
    public class OfferDiscountService
    {
        private readonly IEnumerable<IOffer> _offers;
        public OfferDiscountService(IEnumerable<IOffer> offers) {
            _offers = offers;
        }
        /// <summary>
        /// Calculate Discount using offer code
        /// </summary>
        /// <param name="package"></param>
        /// <param name="cost"></param>
        /// <returns></returns>
        public double CalculateDiscount(Package package, double cost)
        {
            foreach(var offer in _offers)
            {
                if (package.OfferCode == offer.OfferCode && offer.IsCriteriaSatisfied(package))
                {
                    return cost * offer.DiscountPercent / 100;
                }
            }

            return 0;
        }

    }
}
