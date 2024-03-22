using AutoMapper;
using CarShopping.Data.ValueObjects;
using CarShopping.Model;

namespace CarShopping.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CarDealerVO, CarDealer>();
                config.CreateMap<CarDealer, CarDealerVO>();
                config.CreateMap<Car, CarVO>();
                config.CreateMap<CarVO, Car>();
            });
            return mappingConfig;
        }
    }
}
