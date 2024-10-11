


namespace EticaretSite.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string? ProductCode { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public string? Brand { get; set; }
        public string? ImageUrl { get; set; }


    }
}
