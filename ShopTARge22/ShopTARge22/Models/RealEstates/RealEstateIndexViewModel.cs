namespace ShopTARge22.Models.RealEstates
{
    public class RealEstateIndexViewModel
    {
        public Guid? Id { get; set; }
        public string Address { get; set; }
        public float SizeSqrt { get; set; }
        public string BuildingType { get; set; }
        public DateTime BuiltInYear { get; set; }
    }
}