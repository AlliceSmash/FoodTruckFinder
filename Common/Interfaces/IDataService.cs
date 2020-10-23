using System;
using System.Threading.Tasks;
using Common.Models;

namespace Common.Interfaces
{
    public interface IDataService
    {
        GetFoodTruckResponse GetFoodTruckList(DateTime dateTime);
        Task<GetFoodTruckResponse> GetFoodTruckListAsync(DateTime dateTime);
    }
}
