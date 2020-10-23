using System;
namespace Common.Models
{
    public class GetFoodTruckResponse
    {
        public ResponseStatus Status { get; set; }
        public System.Collections.Generic.IEnumerable<FoodTruck> FoodTruckList { get; set; }
    }

    public enum ResponseStatus
    {
        Success =0,
        Timeout = 1,
        Error =2
    }
}
