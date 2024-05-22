using CarShopping.Web.Models;
using CarShopping.Web.Services.IServices;
using CarShopping.Web.Services.Utils;

namespace CarShopping.Web.Services
{
    public class CarDealerService : ICarDealerService
    {
        private readonly HttpClient _client;
        public const string BasePath = "api/dealer";

        public CarDealerService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<CarDealerModel> GetCarDealer()
        {
            var response = await _client.GetAsync(BasePath + "/CarDealer");
            return await response.ReadContentAs<CarDealerModel>();
        }
    }
}
