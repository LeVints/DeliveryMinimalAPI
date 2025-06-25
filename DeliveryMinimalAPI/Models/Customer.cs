using System.Collections.Generic;

namespace DeliveryMinimalAPI.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public bool Active { get; set; }
        public IReadOnlyList<Order>? Orders { get; set; }
    }
} 