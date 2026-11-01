namespace BookManagement.Domain.Entities
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Biography { get; set; }
    }
}
