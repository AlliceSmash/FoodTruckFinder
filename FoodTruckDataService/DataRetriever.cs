using System;
using System.Collections.Generic;
using Common.Interfaces;
using Common.Models;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Globalization;

namespace FoodTruckDataService
{
    public class DataRetriever : IDataService
    {
        private readonly IHttpClientFactory _clientFactory;
        public DataRetriever(IHttpClientFactory httpClientFactory)
        {
            if (httpClientFactory == null) throw new ArgumentNullException();
            _clientFactory = httpClientFactory;

        }

        public GetFoodTruckResponse GetFoodTruckList(DateTime dateTime)
        {
            var currentTime = DateTime.Now.AddSeconds(5);
            var dayOrder = (int)currentTime.DayOfWeek;
            var currentTimeString = currentTime.ToString("t");
        
           // var queryLowerBound = string.Concat(currentHour, ":", currentMinuteLowerBound);
           // var queryUpperBound = string.Concat(currentHour, ":", currentMinuteUppderBound);
            //throw new NotImplementedException();
            var response = new GetFoodTruckResponse
            {
                Status = ResponseStatus.Success,
                FoodTruckList = new List<FoodTruck> {
                    new FoodTruck { Address = "fake", Name = "lot" } ,
                    new FoodTruck { Address = "fake", Name = "lot2" } ,
                    new FoodTruck { Address = "fake2", Name = "lot3" } ,
                    }
            };

            return response;
        }

        public async Task<GetFoodTruckResponse> GetFoodTruckListAsync(DateTime dateTime)
        {
            var currentTime = DateTime.Now.AddSeconds(1);
            var dayOrder = (int)currentTime.DayOfWeek;            
            var currentTimeString = currentTime.ToString("t", DateTimeFormatInfo.InvariantInfo);

            var baseUri = "https://data.sfgov.org/resource/jjew-r69b.json";
            var queryString = $"$where=start24<'{currentTimeString}' AND end24>='{currentTimeString}' AND dayorder={dayOrder}&$select=applicant AS Name,location AS Address&$order=applicant ASC";
            var request = new HttpRequestMessage(HttpMethod.Get, $"{baseUri}?{queryString}");
            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var results = await JsonSerializer.DeserializeAsync
                    <IEnumerable<FoodTruck>>(responseStream);

                return new GetFoodTruckResponse
                {
                    Status = ResponseStatus.Success,
                    FoodTruckList = results
                };
            }
            else
            {
                return new GetFoodTruckResponse
                {
                    FoodTruckList = null,
                    Status = ResponseStatus.Error
                };
            }

        }
    }
}
