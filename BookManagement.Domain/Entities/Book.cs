namespace BookManagement.Domain.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int AuthorId { get; set; }
        public string? Description { get; set; }
        public string ISBN { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal UnitDiscountAmount { get; set; }
        public int CategoryId { get; set; }
        public int? MaxQuantity { get; set; }
        public bool IsActive { get; set; }
        public Author Author { get; set; } = null!;
        public Category Category { get; set; } = null!;
    }
}
