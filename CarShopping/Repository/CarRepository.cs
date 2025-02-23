using AutoMapper;
using CarShopping.Data.ValueObjects;
using CarShopping.Model;
using CarShopping.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace CarShopping.Repository
{
    public class CarRepository : ICarRepository
    {
        private readonly MySQLContext _context;
        private IMapper _mapper;

        public CarRepository(MySQLContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CarVO> Create(CarVO vo)
        {
            Car car = _mapper.Map<Car>(vo);
            _context.Car.Add(car);
            await _context.SaveChangesAsync();
            return _mapper.Map<CarVO>(car);
        }
        public async Task<CarVO> Update(CarVO vo)
        {
            Car car = _mapper.Map<Car>(vo);
            _context.Car.Update(car);
            await _context.SaveChangesAsync();
            return _mapper.Map<CarVO>(car);

        }

        public async Task<bool> Delete(Guid Id)
        {
            try
            {
                Car car = await _context.Car.Where(x => x.Id == Id).FirstOrDefaultAsync();
                if (car == null) return false; 
                _context.Car.Remove(car);
                await _context.SaveChangesAsync(); 
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public async Task<IEnumerable<CarVO>> FindAll()
        {
            List<Car> listCar = await _context.Car.ToListAsync();
            return _mapper.Map<List<CarVO>>(listCar);
        }

        public async Task<CarVO> FindById(Guid id)
        {
            Car car = await _context.Car.Where(x => x.Id == id).FirstOrDefaultAsync();
            return _mapper.Map<CarVO>(car);
        }

        public async Task<IEnumerable<CarVO>> FindByDealership(Guid ownerId)
        {
            var carsByDealership = await _context.Car.Where(c => c.OwnerId == ownerId).ToListAsync();

            var cars = _mapper.Map<List<CarVO>>(carsByDealership);

            return cars;
        }

        //public async Task<IEnumerable<CarVO>> DownloadCarsAsTextFile()
        //{
        //    IEnumerable<CarVO> cars = FindAll().Result;
        //    return cars;
        //}
    }
}
