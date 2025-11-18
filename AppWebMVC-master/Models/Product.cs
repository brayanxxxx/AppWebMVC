namespace WebAppProductsandCategories.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; } // Foreign key for Category

        public Category? Category { get; set; }  // Navigation property
    }
}
