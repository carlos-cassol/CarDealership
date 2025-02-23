using CarShopping.Model.Base;

namespace CarShopping.Model
{

    public class CarParts : BaseEntity
    {
        public string PieceName { get; set; }
        public List<string> FitsBrands { get; set; }
        public List<string> FitsModels { get; set; }
        public double Value { get; set; }
        public string Url { get; set; }
        public string Image { get; set; }
    }
}
