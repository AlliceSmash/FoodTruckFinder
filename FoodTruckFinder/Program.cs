using System;
using Common.Interfaces;
using FoodTruckDataService;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Common.Models;
using System.Threading.Tasks;

namespace FoodTruckFinder
{
    class  Program
    {
        private static readonly int _batchSize = 50;
        static async Task Main(string[] args)
        {

            var serviceprovider = new ServiceCollection()
               .AddSingleton<IDataService, DataRetriever>()
               .AddHttpClient()
               .BuildServiceProvider();

            var dataService = serviceprovider.GetService<IDataService>();

            Console.WriteLine("Hello, we are going to display food trucks that are open currenly");
            Console.WriteLine($"By default, we display {_batchSize} results at a time, please press any key other than Q/q to display more.");
            Console.WriteLine("Or press Q or q to end the program");
              
            var result = await dataService.GetFoodTruckListAsync(DateTime.Now);

            if(result.Status== Common.Models.ResponseStatus.Success)
            {
                HandleSuccess(result, _batchSize);
            }
            else
            {

            }
           
        }

        private static void HandleSuccess(GetFoodTruckResponse result, int batchSize = 10)
        {
            bool endApp = false;
            var nDisplayed = 0;
            var totalTruckCount = result.FoodTruckList.Count();
            Console.WriteLine($"There are {totalTruckCount} food trucks open now. ");
            while (!endApp && totalTruckCount>0)
            {

                var displayList = result.FoodTruckList.Skip(nDisplayed);
               
                displayList = displayList.Take(batchSize);
                nDisplayed += displayList.Count();
                endApp = endApp || displayList.Count() < batchSize;
                foreach (var foodTruck in displayList)
                {
                    Console.WriteLine($" { foodTruck.Name} {foodTruck.Address} ");

                }
                Console.WriteLine("-----------------------------------------------------");

                if (displayList.Count() < batchSize)
                {
                    Console.WriteLine("There is no more food truck to display.");
                    return;
                }

                Console.WriteLine("Please press any key for next batch or press Q to exit");
                var input = Console.ReadLine();
                if (string.Equals(input, "q", StringComparison.CurrentCultureIgnoreCase))
                {
                    endApp = true;
                };

            }

        }
    }
}
