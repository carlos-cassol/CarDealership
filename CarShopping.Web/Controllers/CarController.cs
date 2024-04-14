using CarShopping.Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace CarShopping.Web.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService ?? throw new ArgumentNullException(nameof(carService));
        }

        public async Task<IActionResult> CarIndex()
        {
            var cars = await _carService.FindAllCars();
            return View(cars);
        }
    }
}
