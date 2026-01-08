using Xunit;
using Moq;
using System.Collections.Generic;
using System.Linq;
using CourierService.Services;
using CourierService.Models;
using CourierService.Interfaces;

namespace CourierService.Tests.Services
{
    public class OfferDiscountServiceTests
    {
        [Theory]
        [InlineData("OFR001", 100, 10)]   // Valid offer → 10% discount
        [InlineData("INVALID", 100, 0)]   // Invalid offer → no discount
        [InlineData(null, 100, 0)]        // No offer code → no discount
        public void CalculateDiscount_ShouldReturnDiscount_WhenOfferIsValid(string offerCode, double cost, double expectedDiscount)
        {
            // Arrange
            var package = new Package
            {
                Id = "PKG1",
                OfferCode = offerCode,
                Weight = 50,
                Distance = 30
            };

            var mockOffer = new Mock<IOffer>();
            mockOffer.Setup(o => o.OfferCode).Returns(offerCode);
            mockOffer.Setup(o => o.DiscountPercent).Returns(expectedDiscount);
            mockOffer.Setup(o => o.IsCriteriaSatisfied(package)).Returns(true);

            var offers = new List<IOffer> { mockOffer.Object };
            var service = new OfferDiscountService(offers);

            // Act
            var discount = service.CalculateDiscount(package, cost);

            // Assert
            Assert.Equal(expectedDiscount, discount);
        }

        [Theory]
        [InlineData("OFR002",60,110, 7, 1162.5)]
        [InlineData("OFR003",10,100, 5, 665)]
        public void CalculateDeliveryCost_ShouldReturnCorrectTotalCost(string offerCode,double weight, double distance, double expectedDiscount, double totalCost)
        {
            // Arrange
            var package = new Package
            {
                Id = "PKG1",
                OfferCode = offerCode,
                Weight = weight,
                Distance = distance
            };

            var mockOffer = new Mock<IOffer>();
            mockOffer.Setup(o => o.OfferCode).Returns(offerCode);
            mockOffer.Setup(o => o.DiscountPercent).Returns(expectedDiscount);
            mockOffer.Setup(o => o.IsCriteriaSatisfied(It.IsAny<Package>()))
                     .Returns(true);

            var input = new InputRequestModel
            {
                BaseDeliveryCost = 100,
                Packages = new List<Package> { package }
            };

            var service = new OfferDiscountService(new[] { mockOffer.Object });

            // Act
            var result = service.CalculateDeliveryCost(input);

            // Assert
            Assert.Single(result);
            Assert.Equal("PKG1", result[0].PackageId);
            Assert.Equal(expectedDiscount, result[0].Discount);
            Assert.Equal(totalCost, result[0].TotalCost);
        }

        
        [Theory]
        [InlineData("OFR002", 60, 10, 7, 1162.5)] // No offer eligible
        [InlineData("OFR003", 10, 10, 5, 665)] // No offer eligible
        public void CalculateDiscount_ShouldReturnZero_WhenCriteriaNotSatisfied(string offerCode, double weight, double distance, double expectedDiscount, double totalCost)
        {
            // Arrange
            var package = new Package
            {
                Id = "PKG1",
                OfferCode = offerCode,
                Weight = weight,
                Distance = distance
            };

            var mockOffer = new Mock<IOffer>();
            mockOffer.Setup(o => o.OfferCode).Returns(offerCode);
            mockOffer.Setup(o => o.DiscountPercent).Returns(expectedDiscount);
            mockOffer.Setup(o => o.IsCriteriaSatisfied(It.IsAny<Package>()))
                     .Returns(false);

            var input = new InputRequestModel
            {
                BaseDeliveryCost = 100,
                Packages = new List<Package> { package }
            };

            var service = new OfferDiscountService(new[] { mockOffer.Object });

            // Act
            var result = service.CalculateDeliveryCost(input);

            // Assert
            Assert.Single(result);
            Assert.Equal("PKG1", result[0].PackageId);
            Assert.Equal(0, result[0].Discount);
            Assert.NotEqual(totalCost, result[0].TotalCost);
        }
    }
}
