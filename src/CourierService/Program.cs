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
                decimal baseDeliveryCost = decimal.Parse(Console.ReadLine());
                Console.WriteLine("Enter no of packages");
                int noOfPackages = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter pkg id ");
                string packageId = Console.ReadLine();
                Console.WriteLine("Enter pkg weight ");
                decimal packageWeight = decimal.Parse(Console.ReadLine());
                Console.WriteLine("Enter distance");
                decimal distance = decimal.Parse(Console.ReadLine());
                Console.WriteLine("Enter offer code");
                string offerCode = Console.ReadLine();
                Package package = new Package
                {
                    Id = packageId,
                    Weight = packageWeight,
                    OfferCode = offerCode,
                    Distance = distance
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine("Please enter valid input");
            }

        }
    }
}
