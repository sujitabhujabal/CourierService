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
        public OfferDiscountService(IEnumerable<IOffer> offers)
        {
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
            foreach (var offer in _offers)
            {
                if (package.OfferCode == offer.OfferCode && offer.IsCriteriaSatisfied(package))
                {
                    //Only 1 offer code can be applied 
                    return cost * offer.DiscountPercent / 100;
                }
            }

            return 0;
        }
        /// <summary>
        /// Calculate Delivery Cost by applying offer code
        /// </summary>
        /// <param name="package"></param>
        /// <param name="cost"></param>
        /// <returns></returns>
        public DeliveryCostResult CalculateDeliveryCost(Package package, double cost)
        {
            double discount = CalculateDiscount(package, cost);
            double deliveryCost = cost + (package.Weight * 10) + (package.Distance * 5);
            double totalCost = deliveryCost - (deliveryCost * discount / 100);
            DeliveryCostResult deliveryCostResult = new DeliveryCostResult { 
                 Discount = discount,
                 PackageId = package.Id,
                TotalCost = totalCost

            };
            return deliveryCostResult;
        }

    }
}
