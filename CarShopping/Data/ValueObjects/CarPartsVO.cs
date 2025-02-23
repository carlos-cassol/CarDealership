namespace CarShopping.Data.ValueObjects
{
    public class CarPartsVO
    {
        public string PieceName { get; set; }
        public IEnumerable<string> FitsBrands { get; set; }
        public IEnumerable<string> FitsModels { get; set; }
        public double Value { get; set; }
        public string Url { get; set; }
        public string Image { get; set; }
    }
}
