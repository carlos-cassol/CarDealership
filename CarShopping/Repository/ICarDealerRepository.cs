using CarShopping.Data.ValueObjects;

namespace CarShopping.Repository
{
    public interface ICarDealerRepository
    {
        Task<CarDealerVO> Create(CarDealerVO vo);
        Task<CarDealerVO> Update(CarDealerVO vo);
        Task<CarDealerVO> UpdateValues(Guid id);
        Task<CarDealerVO> Find(Guid id);
        //Task<> GenerateFile();

    }
}
