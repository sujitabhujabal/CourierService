using CourierService.Interfaces;
using CourierService.Models;
using CourierService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourierService
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Enter base delivery cost ");
                double baseDeliveryCost = double.Parse(Console.ReadLine());
                Console.WriteLine("Enter no of packages");
                int noOfPackages = int.Parse(Console.ReadLine());
                while (true)
                {
                    Console.WriteLine("Enter pkg id ");
                    string packageId = Console.ReadLine();
                    Console.WriteLine("Enter pkg weight ");
                    double packageWeight = double.Parse(Console.ReadLine());
                    Console.WriteLine("Enter distance");
                    double distance = double.Parse(Console.ReadLine());
                    Console.WriteLine("Enter offer code");
                    string offerCode = Console.ReadLine();
                    Package package = new Package
                    {
                        Id = packageId,
                        Weight = packageWeight,
                        OfferCode = offerCode,
                        Distance = distance
                    };

                    IOfferRepository offerRepository = new OfferRepository();
                    var offers = offerRepository.GetOffers();

                    OfferDiscountService offerDiscountService = new OfferDiscountService(offers);
                    var discount = offerDiscountService.CalculateDiscount(package, baseDeliveryCost);
                    var deliveryCost = offerDiscountService.CalculateDeliveryCost(baseDeliveryCost, packageWeight, distance, discount, packageId);
                    Console.WriteLine($"PackageId: {deliveryCost.PackageId}, Discount: {deliveryCost.Discount}, TotalCost: {deliveryCost.TotalCost}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Please enter valid input");
            }

        }
    }
}
