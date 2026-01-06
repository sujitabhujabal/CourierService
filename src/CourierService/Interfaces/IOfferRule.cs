using CourierService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourierService.Interfaces
{    public interface IOfferRule
    {
        string OfferCode { get; }
        double DiscountPercent { get; }
        bool IsCriteriaSatisfied(Package pkg);
    }

}
