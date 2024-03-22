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
                _context.CarDealer.Add(carDealer);
                _context.SaveChangesAsync();
                return _mapper.Map<CarDealerVO>(carDealer);

        }
        public async Task<CarDealerVO> Update()
        {
            var carList = _context.Car.ToList();
            CarDealer carDealer = _context.CarDealer.FirstOrDefault();

            if (carDealer != null)
            {
                carDealer.CarQuantity = carList.Count;
                carDealer.MonthRevenue = carList.Sum(x => x.SellingValue);
                carDealer.AmountAvaliableCars = carList.Where(x => x.IsAvaliable).Count();

                _context.CarDealer.Update(carDealer);
                _context.SaveChanges();
            }

            return _mapper.Map<CarDealerVO>(carDealer);
        }

        public async Task<CarDealerVO> Find()
        {
            Update();
            var carDealer = _context.CarDealer.FirstOrDefault();
            return _mapper.Map<CarDealerVO>(carDealer);
        }

    }
}
