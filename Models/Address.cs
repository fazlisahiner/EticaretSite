namespace EticaretSite.Models
{
    public class Address
    {
        public int AddressId { get; set; }
        public int UserId { get; set; }
        public int CountryId { get; set; }
        public int CityId { get; set; }
        public int TownId { get; set; }
        public int? DistrictId { get; set; }
        public int NeighbourhoodId { get; set; }
        public string AddressText { get; set; }
    }
}
