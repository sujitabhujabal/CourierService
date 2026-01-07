using CourierService.Interfaces;
using CourierService.Models;
using CourierService.Services;
using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Services;

namespace CourierService
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string exit = "";
                List<Package> packages = new List<Package>();
                var inputRequestModel = GetCostAndNoOfPackges("Enter base delivery cost, No of packages");
                int pkgCount = 0;

                Console.WriteLine("Enter package details(id weight distance offer_code):");
                while (pkgCount < inputRequestModel.NoOfPackages)
                {
                    pkgCount++;
                    if (exit.ToLower() == "exit")
                    {
                        break;
                    }
                    var packageDetails = InputPackageDetails(inputRequestModel);
                }
                inputRequestModel.Packages = inputRequestModel.Packages;
                IOfferRepository offerRepository = new OfferRepository();
                var offers = offerRepository.GetOffers();

                OfferDiscountService offerDiscountService = new OfferDiscountService(offers);
                var deliveryCostResults = offerDiscountService.CalculateDeliveryCost(inputRequestModel);

                foreach (var deliveryCostResult in deliveryCostResults)
                {
                    Console.WriteLine($"{deliveryCostResult.PackageId} {deliveryCostResult.Discount} {deliveryCostResult.TotalCost}");
                }
                Console.WriteLine("Type Exit to stop or press enter key to continue");
                exit = Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong");
                //Log the exception
                Console.WriteLine(ex.ToString());
            }
        }

        private static InputRequestModel InputPackageDetails(InputRequestModel inputRequestModel)
        {
            var input = Console.ReadLine().Split(' ');
            int noOFInputFields = input.Length;
            Package package = new Package();
            //Package Id
            package.Id = noOFInputFields > 0 ? input[0] : "";

            //Weight
            if (noOFInputFields > 1 && double.TryParse(input[1], out double weight))
            {
                package.Weight = weight;
            }

            //distance
            if (noOFInputFields > 2 && double.TryParse(input[2], out double distance))
            {
                package.Distance = distance;
                //return inputRequestModel;
            }

            //Offer code
            package.OfferCode = noOFInputFields > 3 ? input[3] : "";
            if (package != null)
            {
                if (inputRequestModel.Packages == null)
                {
                    inputRequestModel.Packages = new List<Package>();
                }
                inputRequestModel.Packages.Add(package);
            }
            return inputRequestModel;
        }
        
        private static InputRequestModel GetCostAndNoOfPackges(string message)
        {
            InputRequestModel inputRequestModel = new InputRequestModel();

            while (true)
            {
                Console.WriteLine(message);
                var input = Console.ReadLine().Split(' ');
                int noOFInputFields = input.Length;
                //Validate base cost
                if (noOFInputFields > 0 && double.TryParse(input[0], out double res))
                {
                    inputRequestModel.BaseDeliveryCost = res;
                }

                //Validate No of packages
                if (noOFInputFields > 1 && int.TryParse(input[1], out int noOfPackages))
                {
                    inputRequestModel.NoOfPackages = noOfPackages;
                    return inputRequestModel;
                }
                Console.WriteLine("Invalid number/s, Please enter the delivery cost and the number of packages again");
            }
        }
    }
}
