using CourierService.Interfaces;
using CourierService.Models;
using CourierService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
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
                while (true)
                {
                    if (exit.ToLower() == "exit")
                    {
                        break;
                    }
                    List<Package> packages = new List<Package>();
                    var inputRequestModel = GetCostAndNoOfPackges("Enter base delivery cost, No of packages");
                    int pkgCount = 0;
                    //int vehicleCnt = 0;

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

                    Console.WriteLine("Enter number of vehicles, speed, total carriable weight");
                    var shipmentInputRequestModel = GetShipmentInputs();

                    var vehicles = new List<Vehicle>();

                    //set vehicle details
                    for (int i = 1; i <= shipmentInputRequestModel.TotalVehiclesCount; i++)
                    {
                        vehicles.Add(new Vehicle
                        {
                            Id = i,
                            Speed = shipmentInputRequestModel.MaximumSpeed,
                            MaxLoad = shipmentInputRequestModel.MaxCarriableWeight
                        });
                    }
                    shipmentInputRequestModel.Vehicles = vehicles;

                    inputRequestModel.Packages = inputRequestModel.Packages;
                    IOfferRepository offerRepository = new OfferRepository();
                    var offers = offerRepository.GetOffers();

                    OfferDiscountService offerDiscountService = new OfferDiscountService(offers);
                    var deliveryCostResults = offerDiscountService.CalculateDeliveryCost(inputRequestModel);

                    DeliveryTimeEstimatorService deliveryTimeEstimatorService = new DeliveryTimeEstimatorService();
                    var deliveryTimes = deliveryTimeEstimatorService.EstimatePackageDeliveryTime(inputRequestModel.Packages, shipmentInputRequestModel.Vehicles);


                    var finalResults =
                                     from cost in deliveryCostResults
                                     join time in deliveryTimes
                                         on cost.PackageId equals time.pkgId
                                         into timeGroup
                                     from time in timeGroup.DefaultIfEmpty((pkgId: null, time: 0))
                                     select new DeliveryCostResult
                                     {
                                         PackageId = cost.PackageId,
                                         Discount = cost.Discount,
                                         TotalCost = cost.TotalCost,
                                         DeliveryTime = time.time
                                     };


                    foreach (var deliveryCostResult in finalResults)
                    {
                        Console.WriteLine($"{deliveryCostResult.PackageId} {deliveryCostResult.Discount} {deliveryCostResult.TotalCost} {deliveryCostResult.DeliveryTime}");
                    }
                    Console.WriteLine("Type Exit to stop or press enter key to continue");
                    pkgCount = 0;
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

        private static InputRequestModel InputPackageDetails(InputRequestModel inputRequestModel)
        {
            var input = Console.ReadLine().Split(' ');
            int noOFInputFields = input.Length;
            if (noOFInputFields == 0)
            {
                Console.WriteLine("enter valid input");
            }
            Package package = new Package();
            //Package Id
            package.Id = noOFInputFields > 0 ? input[0] : "";

            //Weight
            if (noOFInputFields > 1 && double.TryParse(input[1], out double weight))
            {
                package.Weight = weight;
            }
            else
            {
                Console.WriteLine("enter valid input");
            }
            //distance
            if (noOFInputFields > 2 && double.TryParse(input[2], out double distance))
            {
                package.Distance = distance;
                //return inputRequestModel;
            }
            else
            {
                Console.WriteLine("enter valid input");
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

        private static ShipmentInputRequest GetShipmentInputs()
        {
            ShipmentInputRequest shipmentInputRequest = new ShipmentInputRequest();

            while (true)
            {
                var input = Console.ReadLine().Split(' ');
                int noOfInputFields = input.Length;
                //Validate No Of Vehicles
                if (noOfInputFields > 0 && int.TryParse(input[0], out int res))
                {
                    shipmentInputRequest.TotalVehiclesCount = res;
                }
                else
                {
                    Console.WriteLine("Invalid number/s, Please enter the vehicle details again");
                }

                //Validate No of Max Speed
                if (noOfInputFields > 1 && int.TryParse(input[1], out int maxSpeed))
                {
                    shipmentInputRequest.MaximumSpeed = maxSpeed;
                }
                else
                {
                    Console.WriteLine("Invalid number/s, Please enter the vehicle details again");
                }

                //Max Carriable Weight
                if (noOfInputFields > 2 && int.TryParse(input[2], out int maxCarriableWeight))
                {
                    shipmentInputRequest.MaxCarriableWeight = maxCarriableWeight;
                    return shipmentInputRequest;
                }
                else
                {
                    Console.WriteLine("Invalid number/s, Please enter the vehicle details again");
                }

            }

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
                else
                {
                    Console.WriteLine("enter valid input");
                }

                //Validate No of packages
                if (noOFInputFields > 1 && int.TryParse(input[1], out int noOfPackages))
                {
                    inputRequestModel.NoOfPackages = noOfPackages;
                    return inputRequestModel;
                }
                else
                {
                    Console.WriteLine("Invalid number/s, Please enter the delivery cost and the number of packages again");
                }
            }
        }
    }
}
