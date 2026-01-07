using CourierService.Interfaces;
using CourierService.Models;
using CourierService.Services;
using System;

namespace CourierService
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string exit = "";
                double baseDeliveryCost = ReadDouble("Enter base delivery cost");
                double noOfPackages = ReadInt("Enter no of packages");
                while (true)
                { 
                    if (exit.ToLower() == "exit")
                    {
                        break;
                    }
                    Console.WriteLine("Enter pkg id ");
                    string packageId = Console.ReadLine();

                    double packageWeight = ReadDouble("Enter pkg weight");
                    double distance = ReadDouble("Enter distance");

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
                    var deliveryCostResult = offerDiscountService.CalculateDeliveryCost(package, baseDeliveryCost);
                    
                    Console.WriteLine($"{deliveryCostResult.PackageId} {deliveryCostResult.Discount} {deliveryCostResult.TotalCost}");
                    
                    Console.WriteLine("Type Exit to stop or press enter key to continue");
                    exit = Console.ReadLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong");
                //Log the exception
                Console.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// Validate input datatype
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private static int ReadInt(string message)
        {
            while (true)
            {
                Console.WriteLine(message);
                int res;
                if (int.TryParse(Console.ReadLine(), out res))
                    return res;
                Console.WriteLine("Invalid number, please enter again");
            }

        }
        /// <summary>
        /// Validate input datatype
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private static double ReadDouble(string message)
        {
            while (true)
            {
                Console.WriteLine(message);
                double res;
                if (double.TryParse(Console.ReadLine(), out res))
                    return res;
                Console.WriteLine("Invalid number, please enter again");
            }
        }
    }
}
