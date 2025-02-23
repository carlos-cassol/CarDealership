using AutoMapper;
using CarShopping.Data.ValueObjects;
using CarShopping.Model;
using CarShopping.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace CarShopping.Repository
{
    public class CarDealerRepository : ICarDealerRepository
    {
        private readonly MySQLContext _context;
        private IMapper _mapper;

        public CarDealerRepository(MySQLContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CarDealerVO> Create(CarDealerVO vo)
        {
            CarDealer carDealer = _mapper.Map<CarDealer>(vo);
            await _context.CarDealer.AddAsync(carDealer);
            await _context.SaveChangesAsync();
            return _mapper.Map<CarDealerVO>(carDealer);
        }
        public async Task<CarDealerVO> Update(CarDealerVO vo)
        {
            CarDealer carDealer = _mapper.Map<CarDealer>(vo);

            if(!_context.CarDealer.Any(c => c.Id == carDealer.Id))
            {
                _context.CarDealer.Update(carDealer);
                await _context.SaveChangesAsync();
            }

            return _mapper.Map<CarDealerVO>(carDealer);
        }

        public async Task<CarDealerVO> UpdateValues(Guid id)
        {
            var vo = _context.CarDealer.FirstOrDefault(x => x.Id == id);

            if (vo is null) {
                return null!;
            }

            var carList = vo.Cars;
            CarDealer carDealer = _mapper.Map<CarDealer>(vo);

            carDealer.CarQuantity = carList.Count;
            carDealer.MonthRevenue = carList.Sum(x => x.SellingValue);
            carDealer.AmountAvaliableCars = carList.Where(x => x.IsAvaliable).Count();

            _context.CarDealer.Update(carDealer);
            await _context.SaveChangesAsync();

            return _mapper.Map<CarDealerVO>(carDealer);
        }

        public async Task<CarDealerVO> Find(Guid id)
        {
            var dbDealer = _context.CarDealer.FirstOrDefault(x => x.Id == id);
            var dealerVo = _mapper.Map<CarDealerVO>(dbDealer);

            var updatedDealer = await UpdateValues(dealerVo.Id);
            return _mapper.Map<CarDealerVO>(updatedDealer);
        }

        public async Task<List<CarVO>> FindAllCars(Guid id)
        {
            CarDealer? dbDealer = _context.CarDealer.FirstOrDefault(x => x.Id == id);
            ArgumentNullException.ThrowIfNull(dbDealer);
            
            var cars = _mapper.Map<List<CarVO>>(dbDealer.Cars);

            return cars;
        }

    }
}
