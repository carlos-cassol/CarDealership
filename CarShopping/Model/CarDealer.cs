using CarShopping.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CarShopping.Model
{
    [Table("dealership")]
    public class CarDealer : BaseEntity
    {
        [Column("name")]
        [Required]
        public string Name { get; set; }
        [Column("carQuantity")]
        [Required]
        public int CarQuantity { get; set; }
        [Column("employeesQuantity")]
        [Required]
        public int Employees { get; set; }
        [Column("monthRevenue")]
        [Required]
        public double MonthRevenue { get; set; }
        [Column("amountAvaliableCars")]
        [Required]
        public int AmountAvaliableCars { get; set; }

        [JsonIgnore]
        public List<Car>? Cars { get; set; } = new();
    }
}
