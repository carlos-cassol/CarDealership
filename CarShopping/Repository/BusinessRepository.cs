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
