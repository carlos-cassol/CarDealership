using CarShopping.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CarShopping.Model
{
    [Table("car")]
    public class Car : BaseEntity
    {
        [Column("brand")]
        [Required]
        public string Brand { get; set; }
        [Column("name")]
        [Required]
        public string Name { get; set; }
        [Column("description")]
        public string Description { get; set; }
        [Column("mileage")]
        [Required]
        public int Mileage { get; set; }
        [Column("fabricationDate")]
        [Required]
        public DateTime FabricationDate { get; set; }
        [Column("sellingValue")]
        [Required]
        public double SellingValue { get; set; }
        [Column("isSold")]
        [Required]
        public bool IsSold { get; set; }
        [Column("isAvaliable")]
        [Required]
        public bool IsAvaliable { get; set; }
        [ForeignKey("Owner")]
        public Guid OwnerId { get; set; }
        [JsonIgnore]
        public CarDealer? Owner { get; set; }
    }
}
