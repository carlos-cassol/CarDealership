using CarShopping.Web.Models;
using CarShopping.Web.Services.IServices;
using CarShopping.Web.Services.Utils;
using System.Reflection;

namespace CarShopping.Web.Services
{
    public class CarService : ICarService
    {
        private readonly HttpClient _client;
        public const string BasePath = "api/car";

        public CarService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<IEnumerable<CarModel>> FindAllCars()
        {
            var response = await _client.GetAsync(BasePath);
            return await response.ReadContentAs<List<CarModel>>();
        }
        public async Task<CarModel> FindCarById(Guid carId)
        {
            var response = await _client.GetAsync($"{BasePath}/{carId}");
            return await response.ReadContentAs<CarModel>();
        }
        public async Task<CarModel> CreateCar(CarModel model)
        {
            var response = await _client.PostAsJson(BasePath, model);
            if (response.IsSuccessStatusCode) 
                return await response.ReadContentAs<CarModel>();
            else 
                throw new Exception($"ER001: Something went wrong when calling api: {response.ReasonPhrase}");
        }
        public async Task<CarModel> UpdateCar(CarModel model)
        {
            var response = await _client.PutAsJson(BasePath, model);
            if (response.IsSuccessStatusCode) 
                return await response.ReadContentAs<CarModel>();
            else 
                throw new Exception($"ER001: Something went wrong when calling api: {response.ReasonPhrase}");
        }

        public async Task<bool> DeleteCarById(Guid carId)
        {
            var response = await _client.DeleteAsync($"{BasePath}/{carId}");
            if (response.IsSuccessStatusCode) 
                return await response.ReadContentAs<bool>();
            else 
                throw new Exception($"ER001: Something went wrong when calling api: {response.ReasonPhrase}");
        }

    }
}
