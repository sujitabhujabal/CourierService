using CourierService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourierService
{
    public class OfferRepository : IOfferRepository
    {
        public IEnumerable<IOfferRule> GetOffers()
        {
            throw new NotImplementedException();
        }
    }
}
