using System.Collections.Generic;

namespace DeliveryMinimalAPI.Models
{
    public class Part
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public IReadOnlyList<Product>? Products { get; set; }
    }
} 