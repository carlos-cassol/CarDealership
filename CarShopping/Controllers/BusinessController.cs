using CarShopping.Data.ValueObjects;
using CarShopping.Repository;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Data;
using System.Drawing;
using System.Text;

namespace CarShopping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessController : ControllerBase
    {

        private IBusinessRepository _businessRepository;
        private ICarRepository _carRepository;
        public BusinessController(IBusinessRepository businessRepository, ICarRepository carRepository)
        {
            _businessRepository = businessRepository;
            _carRepository = carRepository;
        }

        [HttpGet("export")]
        public async Task<ActionResult<List<CarVO>>> DownloadCarsAsTextFile()
        {
            var cars = await _carRepository.FindAll();

            string fileContent = _businessRepository.GenerateTextFileContent(cars);

            byte[] byteArray = Encoding.UTF8.GetBytes(fileContent);

            HttpContext.Response.Headers.Add("Content-Disposition", "attachment; filename=cars_info.txt");

            return File(byteArray, "text/plain");
        }

        [HttpGet("generateModel")]
        public async Task<ActionResult> ExportModelFile()
        {
            MemoryStream stream = await _businessRepository.ExportModelFile();

            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "BusinessModel.xlsx");
        }

        [HttpPost("import")]
        public async Task<ActionResult<IEnumerable<CarVO>>> RecieveTextFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Nenhum arquivo foi enviado.");
            }

            if (Path.GetExtension(file.FileName).ToLower() != ".xlsx")
            {
                return BadRequest("Só são aceitos arquivos em formato .xlsx");
            }

            try
            {
                var carList = await _businessRepository.RecieveTextFile(file);

                return Ok(carList);
            }

            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"Ocorreu um erro na importação do arquivo: {ex.Message}");
            }
        }
    }
}
