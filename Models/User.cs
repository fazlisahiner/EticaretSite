namespace EticaretSite.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Telnumber1 { get; set; }
        public string Telnumber2 { get; set; }
        public string Gender { get; set; }
        public DateTime Birthdate { get; set; }
        public int Isactive { get; set; }
        public DateTime Createdate { get; set; }
    }
}
