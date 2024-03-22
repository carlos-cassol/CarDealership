using CarShopping.Data.ValueObjects;

namespace CarShopping.Repository
{
    public interface ICarDealerRepository
    {
        Task<CarDealerVO> Create(CarDealerVO vo);
        Task<CarDealerVO> Update();
        Task<CarDealerVO> Find();
        //Task<> GenerateFile();

    }
}
