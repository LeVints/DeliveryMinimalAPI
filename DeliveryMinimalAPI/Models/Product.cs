using System.Collections.Generic;

namespace DeliveryMinimalAPI.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public IReadOnlyList<Order>? Orders { get; set; }
        public IReadOnlyList<Part>? Parts { get; set; }
    }
} 