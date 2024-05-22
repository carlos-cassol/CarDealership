using CarShopping.Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace CarShopping.Web.Controllers
{
    public class CarDealerController : Controller
    {
        private readonly ICarDealerService _carDealerService;

        public CarDealerController(ICarDealerService carDealerService)
        {
            _carDealerService = carDealerService ?? throw new ArgumentNullException(nameof(carDealerService));
        }

        [HttpGet]
        public async Task<IActionResult> CarDealerIndex()
        {
            var carDealer = await _carDealerService.GetCarDealer();
            return View(carDealer);
        }
    }
}
