namespace EticaretSite.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string TelNumber1 { get; set; }
        public string TelNumber2 { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public int IsActive { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
