namespace EticaretSite.Models
{
    public class Emplooyee
    {
        public int EmplooyeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string TelNumber1 { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime HireDate { get; set; }
        public string Position { get; set; }
        public int CountryId { get; set; }
        public int CityId { get; set; }
        public int TownId { get; set; }
        public int DistrictId { get; set; }
        public int NeighbourhoodId { get; set; }
        public string AddressText { get; set; }
    }
}