using CarShopping.Web.Models;

namespace CarShopping.Web.Services.IServices
{
    public interface ICarService
    {
        Task<IEnumerable<CarModel>> FindAllCars();
        Task<CarModel> FindCarById(long carId);
        //Task<CarModel> FindCarByBrand(string brand);
        //Task<CarModel> FindCarByName(string name);
        //Task<CarModel> FindCarByValue(double value);
        Task<CarModel> CreateCar(CarModel model);
        Task<CarModel> UpdateCar(CarModel model);
        Task<bool> DeleteCar(long carId);
    }
}
