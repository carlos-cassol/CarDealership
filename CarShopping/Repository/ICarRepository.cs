using CarShopping.Data.ValueObjects;

namespace CarShopping.Repository
{
    public interface ICarRepository
    {
        Task<IEnumerable<CarVO>> FindAll();
        Task<CarVO> FindById(long id);
        Task<CarVO> Create(CarVO vo);
        Task<CarVO> Update(CarVO vo);
        Task<bool> Delete(long Id);
        //Task<IEnumerable<CarVO>> DownloadCarsAsTextFile();
    }
}
