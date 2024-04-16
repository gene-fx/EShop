namespace CatalogAPI.Models
{
    public class Product
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = default!;

        public List<string> Category { get; set; } = new();

        public string Description { get; set; }

        public string ImageFile { get; set; } = default!;

        public decimal Price { get; set; }
    }
}
