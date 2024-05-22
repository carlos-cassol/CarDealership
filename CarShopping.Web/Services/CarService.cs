using CarShopping.Web.Models;
using CarShopping.Web.Services.IServices;
using CarShopping.Web.Services.Utils;
using Microsoft.AspNetCore.Mvc;

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
            if (response.IsSuccessStatusCode)
            {
                var cars = await response.ReadContentAs<List<CarModel>>();
                return cars;
            }
            else
            {
                throw new Exception($"Failed to retrieve cars. Reason: {response.ReasonPhrase}");
            }
        }
        public async Task<CarModel> FindCarById(long carId)
        {
            var response = await _client.GetAsync($"{BasePath}/{carId}");
            if (response.IsSuccessStatusCode)
            {
                var car = await response.ReadContentAs<CarModel>();
                return car;
            }
            else
            {
                throw new Exception($"Failed to retrieve car with ID {carId}. Reason: {response.ReasonPhrase}");
            }
        }

        public async Task<CarModel> CreateCar(CarModel model)
        {
            var response = await _client.PostAsJson(BasePath, model);
            if (response.IsSuccessStatusCode)
            {
                var createdCar = await response.ReadContentAs<CarModel>();
                return createdCar;
            }
            else
            {
                throw new Exception($"Failed to create car. Reason: {response.ReasonPhrase}");
            }
        }

        public async Task<CarModel> UpdateCar(CarModel model)
        {
            var response = await _client.PutAsJson($"{BasePath}/{model.Id}", model);
            if (response.IsSuccessStatusCode)
            {
                var updatedCar = await response.ReadContentAs<CarModel>();
                return updatedCar;
            }
            else
            {
                throw new Exception($"Failed to update car. Reason: {response.ReasonPhrase}");
            }
        }

        public async Task<bool> DeleteCar(long carId)
        {
            var response = await _client.DeleteAsync($"{BasePath}/{carId}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.ReadContentAs<bool>();
                return result;
            }
            else
            {
                throw new Exception($"Failed to delete car. Reason: {response.ReasonPhrase}");
            }
        }

        public Task<IActionResult> CarIndex(int page = 1)
        {
            throw new NotImplementedException();
        }
    }
}
