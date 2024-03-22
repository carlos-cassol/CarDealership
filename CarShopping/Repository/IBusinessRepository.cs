using CarShopping.Data.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace CarShopping.Repository
{
    public interface IBusinessRepository
    {
        Task<List<CarVO>> DownloadCarsAsTextFile();
    }
}
