using AutoMapper;
using CarShopping.Data.ValueObjects;
using CarShopping.Model;
using CarShopping.Model.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Data;
using System.Drawing;
using System.Text;

namespace CarShopping.Repository
{
    public class BusinessRepository : IBusinessRepository
    {
        private readonly MySQLContext _context;
        private IMapper _mapper;

        public BusinessRepository(MySQLContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<CarVO>> DownloadCarsAsTextFile()
        {
            var cars = await _context.Car.ToListAsync();

            return _mapper.Map<List<CarVO>>(cars);
        }

        public async Task<MemoryStream> ExportModelFile()
        {
            MemoryStream stream = new();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage package = new(stream);

            var ws = package.Workbook.Worksheets.Add("Modelo");

            ws.Cells[1, 1].Value = "Marca";
            ws.Cells[1, 2].Value = "Nome";
            ws.Cells[1, 3].Value = "Descrição";
            ws.Cells[1, 4].Value = "Quilometragem";
            ws.Cells[1, 5].Value = "Data de Fabricação";
            ws.Cells[1, 6].Value = "Valor de Venda";
            ws.Cells[1, 7].Value = "Já foi vendido?";
            ws.Cells[1, 8].Value = "Está disponível?";

            var header = ws.Cells[1, 1, 1, ws.Dimension.End.Column];
            header.Style.Fill.PatternType = ExcelFillStyle.Solid;
            header.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#000000"));
            header.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
            header.Style.HorizontalAlignment = ExcelHorizontalAlignment.Fill;

            package.Save();
            stream.Position = 0;

            return stream;
        }

        public async Task<List<CarVO>> RecieveTextFile(IFormFile file)
        {
            List<CarVO> carList = new();
            MemoryStream stream = new();

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

            foreach (var carVO in carList)
            {
                Car car = _mapper.Map<Car>(carVO);
                _context.Car.Add(car);
            }

            await _context.SaveChangesAsync();
            return carList;

        }
        public string GenerateTextFileContent(IEnumerable<CarVO> cars)
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

        private static DataTable LoadFromExcel(ExcelWorksheet worksheet)
        {
            DataTable dataTable = new();

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

    }
}
