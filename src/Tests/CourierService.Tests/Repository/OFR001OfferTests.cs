using CourierService.Models;
using CourierService.Offers;


namespace CourierService.Tests.Services
{
    public class OFR001OfferTests
    {
        private readonly OFR001Offer _offer = new();

        [Theory]
        [InlineData(70, 10)]
        [InlineData(200, 150)]
        public void Valid_Ranges_ReturnTrue(double distance, double weight)
        {
            var pkg = new Package { Distance = distance, Weight = weight };
            Assert.True(_offer.IsCriteriaSatisfied(pkg));
        }

        [Theory]
        [InlineData(69, 10)]
        [InlineData(201, 10)]
        public void Invalid_Ranges_ReturnFalse(double distance, double weight)
        {
            var pkg = new Package { Distance = distance, Weight = weight };
            Assert.False(_offer.IsCriteriaSatisfied(pkg));
        }
    }
}
