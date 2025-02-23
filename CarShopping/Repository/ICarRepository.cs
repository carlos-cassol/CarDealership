using CarShopping.Data.ValueObjects;

namespace CarShopping.Repository
{
    public interface ICarRepository
    {
        Task<IEnumerable<CarVO>> FindAll();
        Task<CarVO> FindById(Guid id);
        Task<IEnumerable<CarVO>> FindByDealership(Guid id);
        Task<CarVO> Create(CarVO vo);
        Task<CarVO> Update(CarVO vo);
        Task<bool> Delete(Guid Id);
        //Task<IEnumerable<CarVO>> DownloadCarsAsTextFile();
    }
}
