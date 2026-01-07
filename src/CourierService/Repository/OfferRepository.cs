using CourierService.Interfaces;
using CourierService.Offers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourierService
{
    public class OfferRepository : IOfferRepository
    {
        public IEnumerable<IOffer> GetOffers()
        {
            return new List<IOffer>
            {
                new OFR001Offer(),
                new OFR002Offer(),
                new OFR003Offer()
            };
        }
    }
}
