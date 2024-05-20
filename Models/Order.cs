namespace EticaretSite.Models
{
    public class Order 
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public int OrderStatus { get; set; }
        public int EmplooyeeId { get; set; }
    }
}