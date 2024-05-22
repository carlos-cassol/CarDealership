using CarShopping.Web.Models;
using CarShopping.Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CarShopping.Web.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService ?? throw new ArgumentNullException(nameof(carService));
        }

        [HttpGet]
        public async Task<IActionResult> CarIndex(int page = 1)
        {
            const int pageSize = 15; 
            var allCars = await _carService.FindAllCars();
            int startIndex = (page - 1) * pageSize;
            var carsInPage = allCars.Skip(startIndex).Take(pageSize);
            int totalPages = (int)Math.Ceiling((double)allCars.Count() / pageSize);

            ViewBag.PageIndex = page;
            ViewBag.TotalPages = totalPages;

            return View(carsInPage);
        }

        [HttpGet]
        public async Task<IActionResult> AllCars()
        {
            var allCars = await _carService.FindAllCars();
            return View(allCars);
        }

            public async Task<IActionResult> CreateCar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCar(CarModel car)
        {
            if (ModelState.IsValid)
            {
                var response = await _carService.CreateCar(car);
                if (response != null) return RedirectToAction(nameof(CarIndex));
            }
            return View(car);
        }

        public async Task<IActionResult> UpdateCar(long id)
        {
            var car = await _carService.FindCarById(id);
            if (car != null)
            {
                return View(car);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCar(CarModel car)
        {
            if (ModelState.IsValid)
            {
                var response = await _carService.UpdateCar(car);
                if (response != null) return RedirectToAction(nameof(CarIndex));
            }
            return View(car);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FindCarById(long id)
        {
            var car = await _carService.FindCarById(id);
            return View(car);
        }

        public async Task<IActionResult> DeleteCar(long id)
        {
            var model = await _carService.FindCarById(id);
            if (model != null) return View(model);
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCar(CarModel car)
        {
            var response = await _carService.DeleteCar(car.Id);
            if (response != null) return RedirectToAction(nameof(CarIndex));
            return View(car);
        }
    }
}