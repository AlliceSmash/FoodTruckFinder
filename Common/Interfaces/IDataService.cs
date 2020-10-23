using System;
using System.Threading.Tasks;
using Common.Models;

namespace Common.Interfaces
{
    public interface IDataService
    {
        Task<GetFoodTruckResponse> GetFoodTruckListAsync(DateTime dateTime);
    }
}
