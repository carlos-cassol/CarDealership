using AutoMapper;
using CarShopping.Data.ValueObjects;
using CarShopping.Model;
using CarShopping.Model.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarShopping.Repository
{
    public class BusinessRepository : IBusinessRepository
    {
        private MySQLContext _context;
        private IMapper _mapper;

        public async Task<List<CarVO>> DownloadCarsAsTextFile()
        {
            var cars = await _context.Car.ToListAsync();

            return _mapper.Map<List<CarVO>>(cars);
        }
    }
}
