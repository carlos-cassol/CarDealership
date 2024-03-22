using CarShopping.Data.ValueObjects;
using CarShopping.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace CarShopping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessController : Controller
    {
        
        private IBusinessRepository _BusinessRepository;
        private ICarRepository _carRepository;
        public BusinessController(IBusinessRepository businessRepository, ICarRepository carRepository)
        {
            _BusinessRepository = businessRepository ?? throw new
                ArgumentNullException(nameof(businessRepository));
            _carRepository = carRepository;
        }

        [HttpGet("download")]
        public async Task<ActionResult<List<CarVO>>> DownloadCarsAsTextFile()
        {
            var cars = await _carRepository.FindAll();

            string fileContent = GenerateTextFileContent(cars);

            byte[] byteArray = Encoding.UTF8.GetBytes(fileContent);

            HttpContext.Response.Headers.Add("Content-Disposition", "attachment; filename=cars_info.txt");

            return File(byteArray, "text/plain");
        }

        private string GenerateTextFileContent(IEnumerable<CarVO> cars)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Cars Information:");
            sb.AppendLine("----------------");
            foreach (var car in cars)
            {
                sb.AppendLine($"Brand: {car.Brand}, Model: {car.Name}, Description: {car.Description},Year: {car.FabricationDate}, Mileage: {car.Mileage},Value: {car.SellingValue}");
                sb.AppendLine(" ");
            }
            return sb.ToString();
        }
    }
}
