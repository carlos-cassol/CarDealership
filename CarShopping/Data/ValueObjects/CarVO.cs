using CarShopping.Model.Base;

namespace CarShopping.Data.ValueObjects
{
    public class CarVO : BaseEntityVO
    {
        public long Id { get; set; }
        public string Brand { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Mileage { get; set; }
        public DateTime FabricationDate { get; set; }
        public double SellingValue { get; set; }
        public bool IsSold { get; set; }
        public bool IsAvaliable { get; set; }
    }
}