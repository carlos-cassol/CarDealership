using CarShopping.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarShopping.Web.Services.IServices
{
    public interface ICarService
    {
        Task<IEnumerable<CarModel>> FindAllCars();
        Task<CarModel> FindCarById(long carId);
        Task<CarModel> CreateCar(CarModel model);
        Task<CarModel> UpdateCar(CarModel model);
        Task<bool> DeleteCar(long carId);
    }
}
