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

            string fileContent = GenerateTextFileContent(cars);

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
                return BadRequest("No file uploaded");
            }

            if (Path.GetExtension(file.FileName).ToLower() != ".xlsx")
            {
                return BadRequest("Only Excel files are allowed");
            }

            try
            {
                List<CarVO> carList = new();
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    ExcelPackage importedFile = new(stream);
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    ExcelWorksheet carsSheet = importedFile.Workbook.Worksheets[0];
                    var resultTable = LoadFromExcel(carsSheet);

                    foreach (DataRow row in resultTable.Rows)
                    {
                        CarVO car = new CarVO()
                        {
                            Brand = row[0].ToString(),
                            Name = row[1].ToString(),
                            Description = row[2].ToString(),
                            Mileage = int.Parse(row[3].ToString()),
                            FabricationDate = DateTime.Parse(row[4].ToString()),
                            SellingValue = double.Parse(row[5].ToString()),
                            IsSold = Convert.ToBoolean(int.Parse(row[6].ToString())),
                            IsAvaliable = Convert.ToBoolean(int.Parse(row[7].ToString()))
                        };
                        carList.Add(car);
                    }

                }

                await _businessRepository.RecieveTextFile(carList);
               
                return Ok(carList);
            }


            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"Ocorreu um erro na importação do arquivo: {ex.Message}");
            }
        }

        private static DataTable LoadFromExcel(ExcelWorksheet worksheet)
        {
            DataTable dataTable = new DataTable();

            foreach (var firstRowCell in worksheet.Cells[1, 1, 1, worksheet.Dimension.End.Column])
            {
                dataTable.Columns.Add(firstRowCell.Text);
            }

            for (var row = 2; row <= worksheet.Dimension.End.Row; row++)
            {
                var dataRow = dataTable.NewRow();
                for (var col = 1; col <= worksheet.Dimension.End.Column; col++)
                {
                    dataRow[col - 1] = worksheet.Cells[row, col].Text;
                }
                dataTable.Rows.Add(dataRow);
            }

            return dataTable;
        }


        private static string GenerateTextFileContent(IEnumerable<CarVO> cars)
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
