using System.ComponentModel.DataAnnotations.Schema;

namespace CarShopping.Web.Models
{
    public class CarDealerModel
    {
        public string Name { get; set; }
        public int CarQuantity { get; set; }
        public int Employees { get; set; }
        public double MonthRevenue { get; set; }
        public int AmountAvaliableCars { get; set; }
    }
}
