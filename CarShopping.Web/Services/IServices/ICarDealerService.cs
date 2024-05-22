using CarShopping.Web.Models;

namespace CarShopping.Web.Services.IServices
{
    public interface ICarDealerService
    {
        Task<CarDealerModel> GetCarDealer();
    }
}
