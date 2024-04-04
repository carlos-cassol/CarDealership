using CarShopping.Data.BaseValueObject;

namespace CarShopping.Data.ValueObjects
{
    public class CarDealerVO : BaseVO
    {
        public string Name { get; set; }
        public int CarQuantity { get; set; }
        public int Employees { get; set; }
        public double MonthRevenue { get; set; }
        public int AmountAvaliableCars { get; set; }
    }
}
