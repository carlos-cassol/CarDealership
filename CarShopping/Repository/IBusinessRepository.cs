using CarShopping.Data.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace CarShopping.Repository
{
    public interface IBusinessRepository
    {
        Task<List<CarVO>> DownloadCarsAsTextFile();
        Task<List<CarVO>> RecieveTextFile(List<CarVO> list);
        Task<MemoryStream> ExportModelFile();
    }
}
