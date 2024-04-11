using AutoMapper;
using CarShopping.Data.ValueObjects;
using CarShopping.Model;
using CarShopping.Model.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;

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

        public async Task<List<CarVO>> RecieveTextFile(List<CarVO> list)
        {

            foreach (var carVO in list)
            {
                Car car = _mapper.Map<Car>(carVO);
                _context.Car.Add(car);
            }

            await _context.SaveChangesAsync();
            return list;

        }
    }
}
